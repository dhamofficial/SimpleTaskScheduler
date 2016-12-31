using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Domain.Models
{
    public class Tracker
    {
        public string Title { get; set; }
        public List<Slot> Slots { get; set; }
        
    }

    public class Slot
    {
        public ScheduleTime Window { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}
