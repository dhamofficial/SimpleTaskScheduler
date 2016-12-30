using SimpleTaskScheduler.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskScheduler.Library.Helper;

namespace SimpleTaskScheduler.Library.Manager
{
    public static class SchedulerExtensions
    {
        private static ScheduleTime CurrentSlot { get; set; }
        private static List<TaskItem> Tasks { get; set; }
        public static ScheduleTime SetCurrentSlot(this ScheduleTime input, List<TaskItem> tasks)
        {
            CurrentSlot = input;
            Tasks = tasks;
            return CurrentSlot;
        }

        public static ScheduleTime ScheduleMatchingTask(this ScheduleTime input)
        {
            if (CurrentSlot == null || Tasks == null) return null;

            var availableTime = CurrentSlot.EndTime.Subtract(CurrentSlot.StartTime).TotalMinutes;

            //if there is no availableTime in current slot = set IsFilled = True
            if (availableTime <= 0) input.IsFilled = true;

            var suitableTasks = Tasks.Where(t => t.Duration.ToInt() >= availableTime);

            CurrentSlot = input;
            return CurrentSlot;
        }
    }
}
