using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Domain.Models
{
    public class ScheduleDay
    {
        public List<ScheduleTime> AvailableSlot { get; set; }
    }
}
