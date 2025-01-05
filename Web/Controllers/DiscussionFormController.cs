using System.Security.Claims;
using Domain.Exception;
using Domain.Model;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.DTO.DiscussionForm;
using Web.Models;

namespace Web.Controllers
{
    public class DiscussionFormController : Controller
    {
        private readonly UserService _userService;
        private readonly DiscussionFormService _discussionFormService;
        private readonly AuthenticationService _authenticationService;

        public DiscussionFormController(UserService userService, DiscussionFormService discussionFormService,
            AuthenticationService authenticationService)
        {
            _userService = userService;
            _discussionFormService = discussionFormService;
            _authenticationService = authenticationService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Overview()
        {
            string emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailClaim))
            {
                return Unauthorized();
            }

            User user = _userService.GetByEmail(emailClaim);
            return View(DiscussionFormBuilder.CreateFromParts(user, _discussionFormService.GetDiscussionForms()));
        }

        public IActionResult Welcome()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiscussionFormCreateRequest request)
        {
            string emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(emailClaim))
            {
                return Unauthorized();
            }
        
            User user = _userService.GetByEmail(emailClaim);
        
            if (ModelState.IsValid)
            {
                try
                {
                    _discussionFormService.CreateDiscussionFrom(user, request);
                    TempData["SuccessMessage"] = "De registratie is succesvol";
                }
                catch (EmailAlreadyExistsException e)
                {
                    ModelState.AddModelError("email", e.Message);
                }
            }
        
            return PartialView("_DiscussionFormPartial",
                DiscussionFormBuilder.CreateFromParts(user, _discussionFormService.GetDiscussionForms()));
        }
    }
}