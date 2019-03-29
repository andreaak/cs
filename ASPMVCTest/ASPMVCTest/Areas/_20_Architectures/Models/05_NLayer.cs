using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.BusinessModels;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.DTO;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.Infrastructure;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.Interfaces;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.BLL.Services;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.DAL.EF;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.DAL.Entities;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.DAL.Interfaces;
using _01_ASPMVCTest.Areas._20_Architectures._05_NLayer.NLayerApp.DAL.Repositories;
using AutoMapper;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._20_Architectures._05_NLayer
{
    namespace NLayerApp.DAL
    {
        namespace Entities
        {
            public class Phone
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Company { get; set; }
                public decimal Price { get; set; }
                public ICollection<Order> Orders { get; set; }
            }

            public class Order
            {
                public int Id { get; set; }
                public decimal Sum { get; set; }
                public string PhoneNumber { get; set; }
                public string Address { get; set; }
                public int PhoneId { get; set; }
                public Phone Phone { get; set; }
                public DateTime Date { get; set; }
            }
        }

        namespace EF
        {
            public class MobileContext : DbContext
            {
                public DbSet<Phone> Phones { get; set; }
                public DbSet<Order> Orders { get; set; }

                static MobileContext()
                {
                    Database.SetInitializer<MobileContext>(new StoreDbInitializer());
                }

                public MobileContext(string connectionString)
                : base(connectionString)
                {
                }
            }

            public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<MobileContext>
            {
                protected override void Seed(MobileContext db)
                {
                    db.Phones.Add(new Phone { Name = "Nokia Lumia 630", Company = "Nokia", Price = 220 });
                    db.Phones.Add(new Phone { Name = "iPhone 6", Company = "Apple", Price = 320 });
                    db.Phones.Add(new Phone { Name = "LG G4", Company = "lG", Price = 260 });
                    db.Phones.Add(new Phone { Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300 });
                    db.SaveChanges();
                }
            }
        }

        namespace Interfaces
        {
            public interface IRepository<T> where T : class
            {
                IEnumerable<T> GetAll();
                T Get(int id);
                IEnumerable<T> Find(Func<T, Boolean> predicate);
                void Create(T item);
                void Update(T item);
                void Delete(int id);
            }

            public interface IUnitOfWork : IDisposable
            {
                IRepository<Phone> Phones { get; }
                IRepository<Order> Orders { get; }
                void Save();
            }
        }

        namespace Repositories
        {
            public class PhoneRepository : IRepository<Phone>
            {
                private MobileContext db;
                public PhoneRepository(MobileContext context)
                {
                    this.db = context;
                }

                public IEnumerable<Phone> GetAll()
                {
                    return db.Phones;
                }

                public Phone Get(int id)
                {
                    return db.Phones.Find(id);
                }

                public void Create(Phone book)
                {
                    db.Phones.Add(book);
                }

                public void Update(Phone book)
                {
                    db.Entry(book).State = EntityState.Modified;
                }

                public IEnumerable<Phone> Find(Func<Phone, Boolean> predicate)
                {
                    return db.Phones.Where(predicate).ToList();
                }

                public void Delete(int id)
                {
                    Phone book = db.Phones.Find(id);
                    if (book != null)
                        db.Phones.Remove(book);
                }
            }

            public class OrderRepository : IRepository<Order>
            {
                private MobileContext db;
                public OrderRepository(MobileContext context)
                {
                    this.db = context;
                }

                public IEnumerable<Order> GetAll()
                {
                    return db.Orders.Include(o => o.Phone);
                }

                public Order Get(int id)
                {
                    return db.Orders.Find(id);
                }

                public void Create(Order order)
                {
                    db.Orders.Add(order);
                }

                public void Update(Order order)
                {
                    db.Entry(order).State = EntityState.Modified;
                }

                public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
                {
                    return db.Orders.Include(o => o.Phone).Where(predicate).ToList();
                }

                public void Delete(int id)
                {
                    Order order = db.Orders.Find(id);
                    if (order != null)
                        db.Orders.Remove(order);
                }
            }

            public class EFUnitOfWork : IUnitOfWork
            {
                private MobileContext db;
                private PhoneRepository phoneRepository;
                private OrderRepository orderRepository;

                public EFUnitOfWork(string connectionString)
                {
                    db = new MobileContext(connectionString);
                }

                public IRepository<Phone> Phones
                {
                    get
                    {
                        if (phoneRepository == null)
                            phoneRepository = new PhoneRepository(db);
                        return phoneRepository;
                    }
                }

                public IRepository<Order> Orders
                {
                    get
                    {
                        if (orderRepository == null)
                            orderRepository = new OrderRepository(db);
                        return orderRepository;
                    }
                }

                public void Save()
                {
                    db.SaveChanges();
                }

                private bool disposed = false;
                public virtual void Dispose(bool disposing)
                {
                    if (!this.disposed)
                    {
                        if (disposing)
                        {
                            db.Dispose();
                        }
                        this.disposed = true;
                    }
                }
                public void Dispose()
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
            }
        }
    }

    namespace NLayerApp.BLL
    {
        namespace DTO
        {
            public class PhoneDTO
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Company { get; set; }
                public decimal Price { get; set; }
            }

            public class OrderDTO
            {
                public int Id { get; set; }
                public string PhoneNumber { get; set; }
                public string Address { get; set; }
                public int PhoneId { get; set; }
                public DateTime? Date { get; set; }
            }
        }

        namespace BusinessModels
        {
            public class Discount
            {
                public Discount(decimal val)
                {
                    _value = val;
                }

                private decimal _value = 0;
                public decimal Value { get { return _value; } }

                public decimal GetDiscountedPrice(decimal sum)
                {
                    if (DateTime.Now.Day == 1)
                        return sum - sum * _value;
                    return sum;
                }
            }
        }

        namespace Infrastructure
        {
            public class ValidationException : Exception
            {
                public string Property { get; protected set; }

                public ValidationException(string message, string prop) : base(message)
                {
                    Property = prop;
                }
            }
        }

        namespace Interfaces
        {
            public interface IOrderService
            {
                void MakeOrder(OrderDTO orderDto);
                PhoneDTO GetPhone(int? id);
                IEnumerable<PhoneDTO> GetPhones();
                void Dispose();
            }
        }

        namespace Services
        {
            public class OrderService : IOrderService
            {
                IUnitOfWork Database { get; set; }
                public OrderService(IUnitOfWork uow)
                {
                    Database = uow;
                }

                public void MakeOrder(OrderDTO orderDto)
                {
                    Phone phone = Database.Phones.Get(orderDto.PhoneId);
                    // валидация
                    if (phone == null)
                        throw new ValidationException("Телефон не найден", "");
                    // применяем скидку
                    decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);
                    Order order = new Order
                    {
                        Date = DateTime.Now,
                        Address = orderDto.Address,
                        PhoneId = phone.Id,
                        Sum = sum,
                        PhoneNumber = orderDto.PhoneNumber
                    };
                    Database.Orders.Create(order);
                    Database.Save();
                }

                public IEnumerable<PhoneDTO> GetPhones()
                {
                    // применяем автомаппер для проекции одной коллекции на другую
                    Mapper.CreateMap<Phone, PhoneDTO>();
                    return Mapper.Map<IEnumerable<Phone>, List<PhoneDTO>>(Database.Phones.GetAll());
                }

                public PhoneDTO GetPhone(int? id)
                {
                    if (id == null)
                        throw new ValidationException("Не установлено id телефона", "");
                    var phone = Database.Phones.Get(id.Value);
                    if (phone == null)
                        throw new ValidationException("Телефон не найден", "");
                    // применяем автомаппер для проекции Phone на PhoneDTO
                    Mapper.CreateMap<Phone, PhoneDTO>();
                    return Mapper.Map<Phone, PhoneDTO>(phone);
                }

                public void Dispose()
                {
                    Database.Dispose();
                }
            }
        }

        namespace Infrastructure
        {
            public class ServiceModule : NinjectModule
            {
                private string connectionString;
                public ServiceModule(string connection)
                {
                    connectionString = connection;
                }

                public override void Load()
                {
                    Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
                }
            }
        }
    }

    namespace NLayerApp.WEB
    {
        namespace Models
        {
            public class PhoneViewModel
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Company { get; set; }
                public decimal Price { get; set; }

            }

            public class OrderViewModel
            {
                public int PhoneId { get; set; }
                public string Address { get; set; }
                public string PhoneNumber { get; set; }
            }
        }

        namespace Util
        {
            public class NinjectDependencyResolver : IDependencyResolver
            {
                private IKernel kernel;
                public NinjectDependencyResolver(IKernel kernelParam)
                {
                    kernel = kernelParam;
                    AddBindings();
                }

                public object GetService(Type serviceType)
                {
                    return kernel.TryGet(serviceType);
                }

                public IEnumerable<object> GetServices(Type serviceType)
                {
                    return kernel.GetAll(serviceType);
                }

                private void AddBindings()
                {
                    kernel.Bind<IOrderService>().To<OrderService>();
                }
            }
        }
    }
}
