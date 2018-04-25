using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestEnhancementQueue.Controllers;
using Domain.Classes;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Tests.Controllers
{
    [TestClass]
    public class ReportRequestControllerTest
    {
        [TestMethod]
        public void Index()
        {
            StakeHolder stakeHolder = new StakeHolder
            {
                StakeHolderId = 1,
                StakeHolderOrganization = "Flabby Organization",
                UserName = "Flabby",
                Password = "fluffybunny"
            };
            ReportRequestController requestController = new ReportRequestController();
            ViewResult result = requestController.Index(stakeHolder) as ViewResult;
            Assert.IsNull(result);
        }
    }
}
