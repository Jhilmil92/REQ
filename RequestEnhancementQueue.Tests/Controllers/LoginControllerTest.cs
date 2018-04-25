using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestEnhancementQueue.Controllers;
using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using BusinessLogicLayer.BLL.Classes;
using System.Web.Mvc;
using Domain.Classes.Req.Domain.ViewModels;

namespace RequestEnhancementQueue.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Login()
        {
            IReqDataSource dataContext = new DataContext();
            IStakeHolderRepository _stakeHolderRepository = new StakeHolderRepository(dataContext);
            IStakeHolderBLL stakeHolderBLL = new StakeHolderBLL(_stakeHolderRepository);
            LoginController loginController = new LoginController(stakeHolderBLL);
            LoginViewModel loginViewModel = new LoginViewModel
            {
                UserName = "flabby",
                Password = "Fluffy Bunny"
            };
            ViewResult viewResult = loginController.Login(loginViewModel) as ViewResult;
            Assert.IsNull(viewResult);
        }

    }
}
