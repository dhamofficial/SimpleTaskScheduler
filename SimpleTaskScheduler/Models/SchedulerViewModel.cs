using SimpleTaskScheduler.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleTaskScheduler.Models
{
    public class SchedulerViewModel
    {
        public List<ScheduleDay> Schedules { get; set; }
        public List<TaskItem> InputTasks { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}