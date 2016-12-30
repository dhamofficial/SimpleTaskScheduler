using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTaskScheduler.Library.Domain.Models;

namespace SimpleTaskScheduler.Library.Domain.Interfaces
{
    public interface ITaskRepository
    {
        List<TaskItem> GetTaskList();
        List<ScheduleDay> GetScheduleWindow();
    }
}
