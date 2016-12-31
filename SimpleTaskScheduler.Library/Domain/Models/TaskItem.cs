using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Domain.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public ScheduleTime ScheduledTime { get; set; }
        public bool IsScheduled { get; set; }
        public bool IsFlexible { get; set; }
    }
}
