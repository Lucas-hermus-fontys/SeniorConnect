using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class DiscussionFormController : Controller
    {
        // [Authorize]
        [HttpGet]
        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}