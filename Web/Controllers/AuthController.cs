using System.Data;
using Domain.Service;
using Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthenticationService _authenticationService = new();
        private readonly UserRepository _userRepository = new();

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;  
            return View(new LoginModel());
        }


        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationService.LoginCredentialsAreValid(model.Email, model.Password))
                {
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/discussionform/overview" : returnUrl);
                }
                ModelState.AddModelError("invalid", "Invalid email or password");
            }

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Logout()
        {

            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegistrationModel());
        }


        [HttpPost]
        public IActionResult Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                _authenticationService.RegisterNewUser(model.Email, model.Password);
                TempData["SuccessMessage"] = "Registration successful!";
            }
            return View(model);
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}
