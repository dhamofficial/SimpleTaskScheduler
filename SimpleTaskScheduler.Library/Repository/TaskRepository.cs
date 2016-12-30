using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleTaskScheduler.Library.Domain.Interfaces;
using SimpleTaskScheduler.Library.Domain.Models;

namespace SimpleTaskScheduler.Library.Repository
{
    public class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// Get list of tasks from json file
        /// </summary>
        /// <returns></returns>
        public List<TaskItem> GetTaskList()
        {
            const string taskListFile = @"c:\tasks.json";
            List<TaskItem> taskList = null;
            if(File.Exists(taskListFile))
            using (var file = File.OpenText(taskListFile))
            {
                var serializer = new JsonSerializer();
                taskList = (List<TaskItem>)serializer.Deserialize(file, typeof(List<TaskItem>));
            }
            int id = 1;
            taskList.ForEach(t=>t.Id=id++);
            return taskList;
        }

        public List<ScheduleDay> GetScheduleWindow()
        {
            var ScheduledDays = new List<ScheduleDay>();
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
            return ScheduledDays;
        }
    }
}
