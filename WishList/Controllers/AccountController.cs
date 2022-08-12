using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel  model)
        {
            if(ModelState.IsValid)
            {
                var appUser = new ApplicationUser();
                appUser.Email = model.Email;
                appUser.UserName = model.Email;
                appUser.PasswordHash = model.Password;
                var result = _userManager.CreateAsync(appUser).Result;
                if(!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(error=>{
                        ModelState.AddModelError(model.Password, error.Description);
                    });
                    return View("Register", model);
                }
                else {
                    return RedirectToAction("Index", "Home"); }
            }
            else
            {
                return View("Register", model);
            }

        }
    }
}
