﻿using System.Diagnostics.Contracts;
using System.Threading;

namespace CSTest._30_NET_Code
{
    internal static class TimeoutHelperNET
    {
        /// <summary>
        /// Returns the EnvironmentNET.TickCount as a start time in milliseconds as a uint, TickCount tools over from postive to negative every ~ 25 days
        /// then ~25 days to back to positive again, uint is sued to ignore the sign and double the range to 50 days
        /// </summary>
        /// <returns></returns>
        public static uint GetTime()
        {
            return (uint) EnvironmentNET.TickCount;
        }

        /// <summary>
        /// Helper function to measure and update the elapsed time
        /// </summary>
        /// <param name="startTime"> The first time (in milliseconds) observed when the wait started</param>
        /// <param name="originalWaitMillisecondsTimeout">The orginal wait timeoutout in milliseconds</param>
        /// <returns>The new wait time in milliseconds, -1 if the time expired</returns>
        public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
        {
            // The function must be called in case the time out is not infinite
            Contract.Assert(originalWaitMillisecondsTimeout != Timeout.Infinite);

            uint elapsedMilliseconds = (GetTime() - startTime);

            // Check the elapsed milliseconds is greater than max int because this property is uint
            if (elapsedMilliseconds > int.MaxValue)
            {
                return 0;
            }

            // Subtract the elapsed time from the current wait time
            int currentWaitTimeout = originalWaitMillisecondsTimeout - (int)elapsedMilliseconds; ;
            if (currentWaitTimeout <= 0)
            {
                return 0;
            }

            return currentWaitTimeout;
        }
    }
}
