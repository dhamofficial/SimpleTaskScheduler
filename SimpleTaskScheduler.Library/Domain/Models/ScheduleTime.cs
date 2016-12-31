using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Domain.Models
{
    public class ScheduleTime
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        private bool _isFilled { get; set; }

        public int GetRemainingTime() {
            return Convert.ToInt32(EndTime.Subtract(StartTime).TotalMinutes);
        }
         
        public bool IsSlotFilled() {
            _isFilled = Convert.ToInt32(EndTime.Subtract(StartTime).TotalMinutes) == 0;
            return _isFilled;
        }
    }
}
