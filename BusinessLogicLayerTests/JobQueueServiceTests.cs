using BusinessLogicLayer.BLL.Classes;
using Domain.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Req.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayerTests
{
    [TestClass]
    public class JobQueueServiceTests
    {
        [TestMethod]
        public void JobQueueService_GetInstance()
        {
            var target = JobQueueService.GetInstance();

            Assert.IsInstanceOfType(target, typeof(JobQueueService));
        }

        [TestMethod]
        public void JobQueueService_Enqueue_OneItem()
        {
            //AAA - Arrange   ACT Assert


            //Arrange
            var job = new Job
            {
              JobId = 1,
              JobDescription = "Cotton Candy Making",
              JobPriority = PriorityLevel.Low
            };
            var target = JobQueueService.GetInstance();

            //Act
            target.Enqueue(job);
            var result = target.PriorityQue.Peek();

            //Assert
            Assert.AreEqual(job, result);

        }

        [TestMethod]
        public void JobQueueService_Enqueue_MultipleItems_AllDifferentProprity()
        {
            //AAA - Arrange   ACT Assert


            //Arrange
            var job = new Job
            {
                JobId = 1,
                JobDescription = "Cotton Candy Making",
                JobPriority = PriorityLevel.Low
            };
            var job1 = new Job
            {
                JobId = 1,
                JobDescription = "Cotton Candy Making",
                JobPriority = PriorityLevel.Medium
            };
            var job2 = new Job
            {
                JobId = 1,
                JobDescription = "Cotton Candy Making",
                JobPriority = PriorityLevel.High
            };
            var target = JobQueueService.GetInstance();

            //Act
            target.Enqueue(job);
            target.Enqueue(job2);
            target.Enqueue(job1);
            var result = target.PriorityQue.Dequeue();
            
            //Assert
            Assert.AreEqual(job2, result);

            var secondndResult=target.PriorityQue.Dequeue();

            Assert.AreEqual(job1, secondndResult);

            var thridResult = target.PriorityQue.Dequeue();

            Assert.AreEqual(job, thridResult);

        }
    }
}
