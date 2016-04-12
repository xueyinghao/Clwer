using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.Core
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}
