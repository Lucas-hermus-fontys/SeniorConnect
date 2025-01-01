using System.Security.Claims;
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

        public DiscussionFormController(UserService userService, GroupChatService groupChatService, DiscussionFormService discussionFormService)
        {
            _userService = userService;
            _discussionFormService = discussionFormService;
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
    }
}