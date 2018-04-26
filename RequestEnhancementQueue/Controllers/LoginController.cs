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
        private readonly ITakerBLL _takerBLL;
        public LoginController()
        {

        }
        public LoginController(IStakeHolderBLL stakeHolderBLL, ITakerBLL takerBLL)
        {
            _stakeHolderBLL = stakeHolderBLL;
            _takerBLL = takerBLL;
        }
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
               // else
               // {
                    //Declare taker's role.
                    //var validTaker = _takerBLL.ValidateLoginCredentials(loginViewModel);
                    //if(validTaker != null)
                    //{
                //Remove Block starts
                    //Taker validTaker = new Taker
                    //{
                    //    TakerId = 1,
                    //    TakerName = "Billy Jane",
                    //    UserName = "billyflabby",
                    //    Password = "billyfluffybunny"
                    //};
                //Remove Block Ends
                       // return RedirectToAction("TakerInformation","Taker",new RouteValueDictionary(validTaker));
                    //}
                 // }
            }
            return View();
        }
    }
}