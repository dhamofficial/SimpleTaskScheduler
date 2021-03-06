﻿using SimpleTaskScheduler.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskScheduler.Library.Domain.Interfaces
{
    public interface ITaskSchedulerManager
    {
        List<TaskItem> GetInputTasks();
        List<TaskItem> Schedule();
        List<ScheduleDay> GetSchedules();
    }
}
