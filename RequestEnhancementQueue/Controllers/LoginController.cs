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
        private readonly IStaffBLL _staffBLL;
        private readonly IClientBLL _clientBLL;
        public LoginController(IStakeHolderBLL stakeHolderBLL, ITakerBLL takerBLL , ILoginManager loginManager)
        {
            _stakeHolderBLL = stakeHolderBLL;
            _takerBLL = takerBLL;
            _loginManager = loginManager;
            _staffBLL = new StaffBLL();
            _clientBLL = new ClientBLL();
        }
        //public LoginController(IStakeHolderBLL stakeHolderBLL, ITakerBLL takerBLL)
        //{
        //    _stakeHolderBLL = stakeHolderBLL;
        //    _takerBLL = takerBLL;
        //}
        // GET: Login
        public ActionResult Register()
        {
            ViewBag.Clients = _clientBLL.GetClients().ToList();
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
                    ModelState.AddModelError("","Please fill in all the fields of the registration form");
                }
            }
            return View();
        }

        public ActionResult RegisterStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterStaff(StaffRegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_loginManager.GetUserByUserName(registrationViewModel.StaffUserName) != null)
                {
                    ModelState.AddModelError("StaffUserName", "UserName already present.");
                    return View();
                }

                if (registrationViewModel.StaffPassword.Equals(registrationViewModel.StaffConfirmPassword))
                {
                    _staffBLL.CreateStaff(registrationViewModel);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Please fill in all the fields of the registration form");
                }
            }
            return View();
        }

        public ActionResult RegisterClient()
        {
            ClientRegistrationViewModel registrationViewModel = new ClientRegistrationViewModel
            {
                JoinDate = DateTime.Now
            };
            return View(registrationViewModel);
        }

        [HttpPost]
        public ActionResult RegisterClient(ClientRegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_loginManager.GetClientByClientName(registrationViewModel.ClientOrganization) != null)
                {
                    ModelState.AddModelError("ClientOrganization", "Client organization already exists!");
                    return View();
                }

                _clientBLL.CreateClient(registrationViewModel);
                return RedirectToAction("StaffInformation","Staff");
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
                Session[Constants.CurrentUserId] = currentUser.UserId;
                if (currentUser.UserType == UserType.StakeHolder)
                {
                    Session[Constants.StakeHolderId] = currentUser.TargetUserID;
                    return RedirectToAction("Index", "ReportRequest", (int)Session[Constants.StakeHolderId]);
                }
                else if(currentUser.UserType == UserType.Taker)
                {
                    Session[Constants.TakerId] = currentUser.TargetUserID;
                    return RedirectToAction("TakerInformation", "Taker", (int)Session[Constants.TakerId]);
                }
                else
                {
                    Session[Constants.StaffId] = currentUser.TargetUserID;
                    return RedirectToAction("StaffInformation", "Staff", (int)Session[Constants.StaffId]);
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