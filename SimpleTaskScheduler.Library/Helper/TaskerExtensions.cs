using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Helper
{
    public static class TaskerExtensions
    {
        public static int ToInt(this string input)
        {
            return Convert.ToInt32(input);
        }
    }
}
