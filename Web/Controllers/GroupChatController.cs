using System.Security.Claims;
using Domain.Model;
using Domain.Service;
using Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.DTO.GroupChat;

namespace Web.Controllers
{
    public class GroupChatController : Controller
    {
        private readonly UserService _userService;
        private readonly GroupChatService _groupChatService;

        public GroupChatController(UserService userService, GroupChatService groupChatService)
        {
            _userService = userService;
            _groupChatService = groupChatService;
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
        
            List<CollaborativeSpace> groupChats = _groupChatService.GetGroupChatsByUserId(user.Id);

            return View(GroupChatBuilder.CreateFromParts(user, groupChats));
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage([FromForm] string message, [FromForm] int groupChatId)
        {
            string emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(emailClaim)) { return Unauthorized(); }
        
            User user = _userService.GetByEmail(emailClaim);
            _groupChatService.CreateMessage(user, message, groupChatId);
        
            return PartialView("_ChatMessagesPartial", GroupChatBuilder.CreateMessagesFromParts(user, _groupChatService.GetGroupChatById(groupChatId)));
        }
 
        [Authorize]
        [HttpGet]
        public IActionResult LoadChatMessages(int groupChatId)
        {
            string emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
        
            if (string.IsNullOrEmpty(emailClaim))
            {
                return Unauthorized();
            }
        
            User user = _userService.GetByEmail(emailClaim);
        
            return PartialView("_ChatMessagesPartial", GroupChatBuilder.CreateMessagesFromParts(user, _groupChatService.GetGroupChatById(groupChatId)));
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}