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
            var skipShortTopics = false;

            if (_preference!=null)
            {
                var pref = _preference();
                skipShortTopics = pref.SkipShortTopics;
            }

            //if SkipShortTopics = true - skip task that has duration 0
            if(skipShortTopics)
            {
                Tasks = Tasks.FindAll(x => x.Duration  > 0);
            }
            else
            {
                var toBeRemoved = Tasks.SingleOrDefault(x => x.Duration ==0);
                if (toBeRemoved != null)
                {
                    Tasks.Remove(toBeRemoved);
                    Tasks.Add(toBeRemoved);
                }
            }
             
            bool slotAvailable = true;
            TimeSpan startTime = CurrentSlot.StartTime;
            while (slotAvailable)
            {
                var availableTime = CurrentSlot.EndTime.Subtract(startTime).TotalMinutes;
                slotAvailable = availableTime > 0;

                if (slotAvailable)
                {
                    var suitableTask = (from task in Tasks
                                        where task.Duration <= availableTime && !task.IsScheduled
                                        select new TaskItem
                                        {
                                            Id=task.Id,
                                            Title = task.Title,
                                            Duration = task.Duration,
                                            IsScheduled = true,
                                            ScheduledTime = new ScheduleTime
                                            {
                                                StartTime = startTime,
                                                EndTime = startTime.Add(new TimeSpan(0, task.Duration, 0))
                                                 
                                            }
                                        }).FirstOrDefault();

                    if (suitableTask == null) break;//if suitableTask is null means there is no further tasks to schedule

                    startTime = startTime.Add(new TimeSpan(0, suitableTask.Duration, 0));
                    Tasks.Where(x => x.Id == suitableTask.Id).ToList().ForEach(a => { a.IsScheduled = true; a.ScheduledTime = suitableTask.ScheduledTime; });
                    scheduledTasks.Add(suitableTask);
                }
                
            } 

            CurrentSlot = input;
            return scheduledTasks;
        }
    }
}
