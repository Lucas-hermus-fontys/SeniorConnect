using System.Data;
using Domain.Service;
using Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly UserRepository _userRepository = new UserRepository();
        
        public AuthController(AuthenticationService jwtService)
        {
            _authenticationService = jwtService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        // [HttpPost]
        // public IActionResult Login(LoginModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         if (_authenticationService.LoginCredentialsAreValid(model.Email, model.Password))
        //         {
        //             DataRow user = _userRepository.GetByEmail(model.Email);
        //             var token = _authenticationService.GenerateToken((int) user["id"], model.Email);
        //             return Redirect($"/discussionform/overview?token={token}");
        //         }
        //         ModelState.AddModelError("invalid", "Ongeldig email of wachtwoord");
        //     }
        //
        //     return View(model);
        // }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationService.LoginCredentialsAreValid(model.Email, model.Password))
                {
                    return Redirect("/discussionform/overview");
                }
                ModelState.AddModelError("invalid", "Ongeldig email of wachtwoord");
            }

            return View(model);
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
            }

            return View(model);
        }


        public IActionResult Welcome()
        {
            return View();
        }
    }
}