using System.Data;
using System.Security.Claims;
using Domain.Service;
using Infrastructure.Database.Repository;
using Infrastructure.Exception;
using Infrastructure.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using AuthenticationService = Domain.Service.AuthenticationService;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly UserService _userService;

        public AuthController(UserService userService, AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = _userService.GetByEmail(model.Email);

            if (_authenticationService.LoginCredentialsAreValid(model.Email, model.Password))
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return Redirect(string.IsNullOrEmpty(returnUrl) ? "/groupchat/overview" : returnUrl);
            }

            ModelState.AddModelError("invalid", "Ongeldig email of wachtwoord");

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
                try
                {
                    _authenticationService.RegisterNewUser(model.Email, model.Password);
                    TempData["SuccessMessage"] = "De registratie is succesvol";
                }
                catch (EmailAlreadyExistsException e)
                {
                    ModelState.AddModelError("email", e.Message);
                }
            }

            return View(model);
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}