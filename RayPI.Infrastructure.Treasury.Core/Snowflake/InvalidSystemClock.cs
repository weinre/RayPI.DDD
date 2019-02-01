using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Snowflake
{
    public class InvalidSystemClock : Exception
    {
        public InvalidSystemClock(string message)
            : base(message)
        {
        }
    }
}
