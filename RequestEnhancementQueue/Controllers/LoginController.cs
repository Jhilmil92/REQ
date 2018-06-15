using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
using Domain.Classes.Req.Domain.ViewModels;
using Req.Enums.Req.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace RequestEnhancementQueue.Controllers
{
    public class LoginController : Controller
    {
        private readonly IStakeHolderBLL _stakeHolderBLL;
        private readonly ITakerBLL _takerBLL;
        private readonly ILoginManager _loginManager;
        public LoginController(IStakeHolderBLL stakeHolderBLL, ITakerBLL takerBLL , ILoginManager loginManager)
        {
            _stakeHolderBLL = stakeHolderBLL;
            _takerBLL = takerBLL;
            _loginManager = loginManager;
        }
        //public LoginController(IStakeHolderBLL stakeHolderBLL, ITakerBLL takerBLL)
        //{
        //    _stakeHolderBLL = stakeHolderBLL;
        //    _takerBLL = takerBLL;
        //}
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(StakeHolderRegistrationViewModel registrationViewModel)
        {
            if(ModelState.IsValid)
            {
                if (_loginManager.GetUserByUserName(registrationViewModel.StakeHolderUserName) != null)
                {
                    ModelState.AddModelError("StakeHolderUserName", "UserName already present.");
                    return View();
                }

                if (registrationViewModel.StakeHolderPassword.Equals(registrationViewModel.StakeHolderConfirmPassword))
                {
                    _stakeHolderBLL.CreateStakeHolder(registrationViewModel);
                    return RedirectToAction("Login");
                }
               
            }
            return View();
        }

        [HttpGet]
        public ActionResult RegisterTaker()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterTaker(TakerRegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_loginManager.GetUserByUserName(registrationViewModel.TakerUserName) != null)
                {
                    ModelState.AddModelError("TakerUserName", "UserName already present.");
                    return View();
                }

                if (registrationViewModel.TakerPassword.Equals(registrationViewModel.TakerConfirmPassword))
                {
                    _takerBLL.CreateTaker(registrationViewModel);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("","Please fill in all the fields of teh registration form");
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //var validStakeHolder = _stakeHolderBLL.ValidateLoginCredentials(loginViewModel);
                //var validTaker = _takerBLL.ValidateLoginCredentials(loginViewModel);

                var currentUser = _loginManager.ValidateCredentials(loginViewModel.UserName,loginViewModel.Password);
                if(currentUser == null)
                {
                    ModelState.AddModelError("","The Username or Password provided is Incorrect");
                }
                if (currentUser.UserType == UserType.StakeHolder)
                {
                    Session[Constants.StakeHolderId] = currentUser.TargetUserID;
                    return RedirectToAction("Index", "ReportRequest", (int)Session[Constants.StakeHolderId]);
                }
                else 
                {
                    Session[Constants.TakerId] = currentUser.TargetUserID;
                    return RedirectToAction("TakerInformation", "Taker", (int)Session[Constants.TakerId]);
                }
               
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
    }
}