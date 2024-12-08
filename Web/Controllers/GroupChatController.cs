using System.Security.Claims;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.DTO.GroupChat;

namespace Web.Controllers
{
    public class GroupChatController : Controller
    {
        private readonly UserService _userService;

        public GroupChatController(UserService userService)
        {
            _userService = userService;
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

            return View(GroupChatBuilder.CreateFromParts(_userService.GetByEmail(emailClaim)));
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}