
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Data;
using TestTaskCrossager.Api.RequestsResponses;
using TestTaskCrossager.Entities;
using TestTaskCrossager.Errors;
using TestTaskCrossager.Exceptions;
using TestTaskCrossager.Helpers;
using TestTaskCrossager.Repositories;
using static TestTaskCrossager.Api.Mappers.GuestGroupMapper;


namespace TestTaskCrossager.Services
{
    public sealed class GuestGroupService : IGuestGroupService
    {
        private readonly RestaurantDbContext _context;
        private readonly TimeProvider _timeProvider;
        private readonly ILogger<GuestGroupService> _logger;

        public GuestGroupService(
            RestaurantDbContext context,
            TimeProvider timeProvider,
            ILogger<GuestGroupService> logger)
        {
            _context = context;
            _timeProvider = timeProvider;
            _logger = logger;
        }

        public async Task<Result<CreateGuestGroupResponse>> CreateAsync(
            int size,
            CancellationToken cancellationToken)
        {
            var retryPipeline = _context.CreateDeadlockRetryPipeline<CreateGuestGroupResponse>();

            try
            {
                return await retryPipeline.ExecuteAsync(async token =>
                {
                    await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, token);

                    var createdGroup = new GuestGroup
                    {
                        Size = size,
                        ArrivedAt = _timeProvider.GetUtcNow().UtcDateTime
                    };

                    var table = await GetProperTable(createdGroup.Size, token);

                    if (table is not null)
                    {
                        createdGroup.Table = table;
                        createdGroup.Status = GuestGroupStatus.Seated;
                    }
                    else
                    {
                        createdGroup.Status = GuestGroupStatus.Waiting;
                    }

                    _context.GuestGroups.Add(createdGroup);

                    await _context.SaveChangesAsync(token);
                    await transaction.CommitAsync(token);

                    return Result<CreateGuestGroupResponse>.Success(MapToCreateGuestGroup(createdGroup));
                }, cancellationToken);

            }
            catch (SqlException exception) when (exception.Number == 1205)
            {
                _logger.LogError(exception, "Failed to create guest group with size {Size} after all deadlock retry attempts.", size);

                return Result<CreateGuestGroupResponse>.Failure(ErrorCode.ServerError, "Database concurrency conflict.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to create guest group with size {Size}.", size);
                return Result<CreateGuestGroupResponse>.Failure(ErrorCode.ServerError, exception.Message);
            }
        }

        public async Task<Result> LeaveAsync(int groupId, CancellationToken cancellationToken)
        {

            try
            {
                int updatedRows = await _context.GuestGroups
                                .Where(group => group.Id == groupId && group.Status == GuestGroupStatus.Waiting)
                                .ExecuteUpdateAsync(setters => setters.SetProperty(group => group.Status, GuestGroupStatus.Left), cancellationToken);

                if (updatedRows == 1)
                {
                    return Result.Success();
                }

                bool groupExists = await _context.GuestGroups
                    .AnyAsync(group => group.Id == groupId, cancellationToken);

                return groupExists
                    ? Result.Failure(ErrorCode.GroupInvalidStatus)
                    : Result.Failure(ErrorCode.GroupNotFound);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to remove group {GroupId} from queue.", groupId);
                return Result.Failure(ErrorCode.ServerError, exception.Message);
            }
        }

        public async Task<Result<CompleteVisitResponse>> CompleteAsync(int groupId, CancellationToken cancellationToken)
        {
            var retryPipeline = _context.CreateDeadlockRetryPipeline<CompleteVisitResponse>();

            try
            {
                return await retryPipeline.ExecuteAsync(async token =>
                {
                    await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, token);

                    var group = await _context.GuestGroups
                        .Include(group => group.Table!)
                        .ThenInclude(table => table.GuestGroups.Where(otherGroup => otherGroup.Status == GuestGroupStatus.Seated))
                        .FirstOrDefaultAsync(group => group.Id == groupId, token);

                    if (group is null)
                    {
                        return Result<CompleteVisitResponse>.Failure(ErrorCode.GroupNotFound);
                    }

                    if (group.Status != GuestGroupStatus.Seated)
                    {
                        return Result<CompleteVisitResponse>.Failure(ErrorCode.GroupInvalidStatus);
                    }

                    if (group.Table is null)
                    {
                        _logger.LogError("Seated group {GroupId} has no table.", groupId);

                        return Result<CompleteVisitResponse>.Failure(ErrorCode.ServerError, "Seated group has no assigned table.");
                    }

                    group.Status = GuestGroupStatus.Completed;

                    int freePlacesCount = group.Table.Capacity - group.Table.GuestGroups
                        .Where(otherGroup => otherGroup.Id != group.Id && otherGroup.Status == GuestGroupStatus.Seated)
                        .Sum(otherGroup => otherGroup.Size);

                    var newlySeatedGroups = await SeatWaitingGroupsAsync(group.Table, freePlacesCount, token);

                    await _context.SaveChangesAsync(token);
                    await transaction.CommitAsync(token);

                    return Result<CompleteVisitResponse>.Success(new CompleteVisitResponse(newlySeatedGroups.Select(MapToNewlySeatedGroup).ToArray()));
                }, cancellationToken);
            }
            catch (SqlException exception) when (exception.Number == 1205)
            {
                _logger.LogError(exception, "Failed to complete visit for group {GroupId} after all deadlock retry attempts.", groupId);

                return Result<CompleteVisitResponse>.Failure(ErrorCode.ServerError, "Database concurrency conflict.");
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to complete visit for group {GroupId}.", groupId);
                return Result<CompleteVisitResponse>.Failure(ErrorCode.ServerError, exception.Message);
            }
        }

        private async Task<IReadOnlyCollection<GuestGroup>> SeatWaitingGroupsAsync(RestaurantTable table, int freePlacesCount, CancellationToken cancellationToken)
        {
            var waitingGroups =
                await _context.GuestGroups
                    .Where(group => group.Status == GuestGroupStatus.Waiting && group.Size <= freePlacesCount)
                    .OrderBy(group => group.ArrivedAt)
                    .ThenBy(group => group.Id)
                    .ToArrayAsync(cancellationToken);

            var newlySeatedGroups = new List<GuestGroup>();

            int leftPlacesCount = freePlacesCount;

            foreach (GuestGroup group in waitingGroups)
            {
                if(group.Size <= leftPlacesCount)
                {
                    leftPlacesCount -= group.Size;
                    group.Status = GuestGroupStatus.Seated;
                    group.Table = table;
                    
                    newlySeatedGroups.Add(group);
                }
                if (leftPlacesCount == 0)
                {
                    break;
                }
            }

            return newlySeatedGroups;
        }

        private async Task<RestaurantTable?> GetProperTable(int groupSize, CancellationToken cancellationToken)
        {
            bool earlierSuitableGroupExists = await HasEarlierSuitableGroupAsync(groupSize, cancellationToken);

            if (earlierSuitableGroupExists)
            {
                return default;
            }

            var tables =
                await _context.Tables
                    .Include(table => table.GuestGroups.Where(group => group.Status == GuestGroupStatus.Seated))
                        .Where(table => (table.Capacity - table.GuestGroups
                            .Where(group => group.Status == GuestGroupStatus.Seated)
                            .Sum(group => group.Size)) >= groupSize)
                    .ToArrayAsync(cancellationToken);


            return GetProperTable(tables);
        }

        private async Task<bool> HasEarlierSuitableGroupAsync(int groupSize,
            CancellationToken cancellationToken)
        {
            return await _context.GuestGroups.AnyAsync(
                group => group.Status == GuestGroupStatus.Waiting && group.Size <= groupSize,
                cancellationToken);
        }

        private static RestaurantTable? GetProperTable(IReadOnlyCollection<RestaurantTable> tables)
        {
            var emptyTable = tables
                            .Where(table => table.GuestGroups.Count == 0)
                            .OrderBy(table => table.Capacity)
                            .FirstOrDefault();


            if (emptyTable is not null)
            {
                return emptyTable;
            }

            var tableStates = tables
                .Select(table =>
                {
                    int availableSeats = table.Capacity - table.GuestGroups.Sum(group => group.Size);

                    return new
                    {
                        Table = table,
                        AvailableSeats = availableSeats
                    };
                })
                .ToArray();

            return tableStates
                .OrderBy(state => state.AvailableSeats)
                .Select(state => state.Table)
                .FirstOrDefault();
        }
    }
}
