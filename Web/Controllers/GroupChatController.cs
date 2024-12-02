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
            return View(GroupChatBuilder.CreateFromParts());
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}