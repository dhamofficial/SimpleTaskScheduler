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
        public delegate SchedulerPreference Preference();

        public static ScheduleTime SetCurrentSlot(this ScheduleTime input, List<TaskItem> tasks)
        {
            CurrentSlot = input;
            Tasks = tasks;
            return CurrentSlot;
        }

        public static List<TaskItem> ProcessThisSlot(this ScheduleTime input, Preference _preference)
        {
            if (CurrentSlot == null || Tasks == null) return null;
            var scheduledTasks = new List<TaskItem>();
            var SkipShortTopics = false;

            if (_preference!=null)
            {
                var pref = _preference();
                SkipShortTopics = pref.SkipShortTopics;
            }

            //if SkipShortTopics = true - skip task that has duration 0
            if(SkipShortTopics)
            {
                Tasks = Tasks.FindAll(x => x.Duration.ToInt() > 0);
            }

            //var availableTime = CurrentSlot.EndTime.Subtract(CurrentSlot.StartTime).TotalMinutes;

            //if there is no availableTime in current slot = set IsFilled = True
            //if (availableTime <= 0) { input.IsFilled = true; }

            bool slotAvailable = true;
            while (slotAvailable)
            {
                var availableTime = CurrentSlot.EndTime.Subtract(CurrentSlot.StartTime).TotalMinutes;
                slotAvailable = availableTime > 0;

                if (slotAvailable)
                {
                    var suitableTask = (from task in Tasks
                                        where task.Duration.ToInt() <= availableTime && !task.IsScheduled
                                        select new TaskItem
                                        {
                                            Id=task.Id,
                                            Title = task.Title,
                                            Duration = task.Duration,
                                            IsScheduled = true,
                                            ScheduledTime = new ScheduleTime
                                            {
                                                StartTime = CurrentSlot.StartTime,
                                                EndTime = CurrentSlot.StartTime.Add(new TimeSpan(0, task.Duration.ToInt(), 0)),
                                                IsFilled=true
                                            }
                                        }).FirstOrDefault();

                    CurrentSlot.StartTime = CurrentSlot.StartTime.Add(new TimeSpan(0, suitableTask.Duration.ToInt(), 0));
                    Tasks.Where(x => x.Id == suitableTask.Id).ToList().ForEach(a => { a.IsScheduled = true; a.ScheduledTime = suitableTask.ScheduledTime; });
                    scheduledTasks.Add(suitableTask);
                }
                
            } 

            CurrentSlot = input;
            return scheduledTasks;
        }
         
    }
}
