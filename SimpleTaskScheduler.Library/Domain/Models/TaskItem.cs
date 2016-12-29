using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Domain.Models
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public ScheduleTime ScheduledTime { get; set; }
    }
}
