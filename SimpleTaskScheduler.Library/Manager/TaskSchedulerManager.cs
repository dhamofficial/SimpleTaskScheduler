using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskScheduler.Library.Domain.Interfaces;
using SimpleTaskScheduler.Library.Domain.Models;
using SimpleTaskScheduler.Library.Helper;
using SimpleTaskScheduler.Library.Repository;

namespace SimpleTaskScheduler.Library.Manager
{
    public class TaskSchedulerManager:ITaskSchedulerManager
    {
        private List<ScheduleDay> ScheduledDays { get; set; }

        public TaskSchedulerManager()
        {
            SetConferenceWindow();
        }

        private void SetConferenceWindow()
        {
            ScheduledDays = new List<ScheduleDay>();
            var Day1 = new ScheduleDay
            {
                AvailableSlot = new List<ScheduleTime>(){
                    new ScheduleTime()
                    {
                        StartTime = new TimeSpan(0,9,0,0), EndTime = new TimeSpan(0,12,0,0)
                    },
                    new ScheduleTime()
                    {
                        StartTime = new TimeSpan(0,13,0,0), EndTime = new TimeSpan(0,18,0,0)
                    }
                }
            };
            ScheduledDays.Add(Day1);
        }


        public void Schedule()
        {
            var taskRepository = new TaskRepository();
            var tasks = taskRepository.GetTaskList();


            ScheduledDays.ForEach(Day=> {
                Day.AvailableSlot.Where(x=>x.IsFilled==false).FirstOrDefault()
                                                                .SetCurrentSlot(tasks.Where(t=>t.IsScheduled==false).ToList())
                                                                .ScheduleMatchingTask();
            });


            //double availableTime = 0;

            //ScheduledDay.AvailableSlot.ForEach(x =>
            //{
            //    availableTime+= x.EndTime.Subtract(x.StartTime).TotalMinutes;
            //});

            //var scheduledTasks = new List<TaskItem>();
            //var tracker = new Tracker();

            //foreach (var task in tasks)
            //{
            //    var taskDuration = task.Duration.ToInt();
            //    if (taskDuration <= 0 || !(availableTime > 0) || !(availableTime >= taskDuration)) continue;
            //    //need to check if the task can be parked within the available slot of the current track
            //    if (ScheduledDay.AvailableSlot.Exists(
            //        x => x.EndTime.Subtract(x.StartTime.Add(new TimeSpan(0, 0, taskDuration))).TotalMinutes > 0))            
            //    {
            //        //task.ScheduledTime = new ScheduleTime { StartTime = x.StartTime, EndTime = x.StartTime.Add(new TimeSpan(0, 0, taskDuration)) };

            //    }

            //    scheduledTasks.Add(task);
            //    availableTime -= taskDuration;
            //}


        }
    }
}
