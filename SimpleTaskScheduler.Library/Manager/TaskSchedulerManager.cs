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
        private List<ScheduleDay> scheduledDays;
        private ITaskRepository taskRepository;

        public TaskSchedulerManager()
        {
            taskRepository = new TaskRepository();
            scheduledDays = taskRepository.GetScheduleWindow();
        }

        public List<TaskItem> GetInputTasks()
        {
            return taskRepository.GetTaskList();
        }

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

            scheduledDays.ForEach(Day=> {
                Day.AvailableSlot.ForEach(eachSlot => {
                    eachSlot
                        .SetCurrentSlot(tasks)
                        .ProcessThisSlot(GetPreference)
                        .ForEach(item=> scheduledTasks.Add(item));
                });
            });
            return scheduledTasks;
        }

        private SchedulerPreference GetPreference()
        {
            var preference = new Domain.Models.SchedulerPreference {
                SkipShortTopics=true
            };
            return preference;
        }
    }
}
