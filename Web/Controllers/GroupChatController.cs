using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.DTO.GroupChat;

namespace Web.Controllers
{
    public class GroupChatController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Overview()
        {
            Console.WriteLine( User.Identity.Name);
            GroupChatDto groupChatDto = new GroupChatDto();
            groupChatDto.User.DisplayName = "Lucas Hermus";
            return View(groupChatDto);
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}