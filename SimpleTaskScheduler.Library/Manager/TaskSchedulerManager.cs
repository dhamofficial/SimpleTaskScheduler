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
        private ITaskRepository taskRepository;

        public TaskSchedulerManager()
        {
            taskRepository = new TaskRepository();
        }

        public List<TaskItem> GetInputTasks()
        {
            return taskRepository.GetTaskList();
        }

        /// <summary>
        /// Returns timing window for slots
        /// </summary>
        /// <returns></returns>
        public List<ScheduleDay> GetSchedules()
        {
            return taskRepository.GetScheduleWindow();
        }

        /// <summary>
        /// Process the list of available slots based on best suitable for the available time within the slot.
        /// </summary>
        public List<TaskItem> Schedule()
        {
            var scheduledTasks=new List<TaskItem>();
            var tasks = taskRepository.GetTaskList();
            var scheduledDays = GetSchedules();

            scheduledDays.ForEach(Day =>
            {
                Day.AvailableSlot.ForEach(eachSlot =>
                {
                    eachSlot
                        .SetCurrentSlot(tasks)
                        .ProcessThisSlot(() => { return new SchedulerPreference { SkipShortTopics = false }; })
                        .ForEach(item => scheduledTasks.Add(item));
                });
            });
             

            return scheduledTasks;
        }
        
    }
}
