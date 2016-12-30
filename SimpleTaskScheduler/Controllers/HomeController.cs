using SimpleTaskScheduler.Library.Domain.Models;
using SimpleTaskScheduler.Library.Manager;
using SimpleTaskScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleTaskScheduler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Scheduler";

            var manager = new TaskSchedulerManager();

            var vm = new SchedulerViewModel() {
                InputTasks = manager.GetInputTasks(),
                Tasks = manager.Schedule(),
                Schedules = manager.GetSchedules()
            };

            return View(vm);
        }
    }
}
