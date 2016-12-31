using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleTaskScheduler.Library.Repository;
using SimpleTaskScheduler.Library.Manager;

namespace UnitTest
{
    [TestClass]
    public class TestSpecs
    {

        [TestMethod]
        public void Sad_Data_Is_Null_Or_Empty()
        {
            //Arrange
            var repository = new TaskRepository();

            //Act
            var tasks = repository.GetTaskList();

            //Assert
            Assert.IsTrue(tasks==null || tasks.Count==0);
        }

        [TestMethod]
        public void Data_Should_Not_Be_Null_Or_Empty()
        {
            //Arrange
            var repository = new TaskRepository();

            //Act
            var tasks = repository.GetTaskList();

            //Assert
            Assert.IsNotNull(repository);
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count>0);
        }

        [TestMethod]
        public void Schedule_Tasks()
        {
            //Arrange
            var repository = new TaskRepository();
            var manager = new TaskSchedulerManager();
            var tasks = repository.GetTaskList();

            //Act
            var scheduledItems = manager.Schedule();

            //Assert
            Assert.IsNotNull(scheduledItems);
            Assert.IsTrue(tasks.Count == scheduledItems.Count);
        }
    }
}
