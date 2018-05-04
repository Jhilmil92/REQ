using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
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
        public LoginController()
        {
            _stakeHolderBLL = new StakeHolderBLL();
            _takerBLL = new TakerBLL();
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
                var validStakeHolder = _stakeHolderBLL.ValidateLoginCredentials(loginViewModel);
                var validTaker = _takerBLL.ValidateLoginCredentials(loginViewModel);

                if (validStakeHolder != null)
                {
                    Session["StakeHolder"] = validStakeHolder;
                    return RedirectToAction("Index", "ReportRequest", (StakeHolder)Session["StakeHolder"]);
                }
                else if(validTaker != null)
                {
                    Session["Taker"] = validTaker;
                    return RedirectToAction("TakerInformation", "Taker", (Taker)Session["Taker"]);
                }
                else
                {
                    ModelState.AddModelError("","The Username or Password provided is Incorrect");
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