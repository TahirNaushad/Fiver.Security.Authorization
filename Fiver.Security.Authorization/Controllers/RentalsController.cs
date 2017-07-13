using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fiver.Security.Authorization.Controllers
{
    [Authorize(Policy = "PaidMember")]
    public class RentalsController : Controller
    {
        public IActionResult Rent()
        {
            return View();
        }

        [Authorize(Policy = "Over18")]
        public IActionResult RentOver18()
        {
            return View();
        }
    }
}
