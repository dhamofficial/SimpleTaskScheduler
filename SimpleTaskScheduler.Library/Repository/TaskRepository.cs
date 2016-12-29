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
            List<TaskItem> taskList;
            using (var file = File.OpenText(taskListFile))
            {
                var serializer = new JsonSerializer();
                taskList = (List<TaskItem>)serializer.Deserialize(file, typeof(List<TaskItem>));
            }

            return taskList;
        }
    }
}
