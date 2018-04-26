using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RequestEnhancementQueue.Controllers
{
    public class LoginController : Controller
    {
        private readonly IStakeHolderBLL _stakeHolderBLL;
        public LoginController()
        {

        }
        public LoginController(IStakeHolderBLL stakeHolderBLL)
        {
            _stakeHolderBLL = stakeHolderBLL;
        }
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel registrationViewModel)
        {
            if(ModelState.IsValid)
            {
                if (registrationViewModel.StakeHolderPassword.Equals(registrationViewModel.StakeHolderConfirmPassword))
                {
                    _stakeHolderBLL.CreateStakeHolder(registrationViewModel);
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //var validStakeHolder = _stakeHolderBLL.ValidateLoginCredentials(loginViewModel);
                //if (validStakeHolder != null)
                //{
                //Remove Block --Starts
                StakeHolder validStakeHolder = new StakeHolder
                {
                    StakeHolderId = 1,
                    StakeHolderOrganization = "Bumble Bee Associates",
                    UserName = "flabby",
                    Password = "fluffybunny"
                };
                 //Ends
                    return RedirectToAction("Index", "ReportRequest", new RouteValueDictionary(validStakeHolder));
                //}
            }
            return View();
        }
    }
}