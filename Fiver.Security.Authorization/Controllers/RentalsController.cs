using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fiver.Security.Authorization.Models.Rentals;
using System.Threading.Tasks;

namespace Fiver.Security.Authorization.Controllers
{
    [Authorize(Policy = "PaidMember")]
    public class RentalsController : Controller
    {
        private readonly IAuthorizationService authService;

        public RentalsController(IAuthorizationService authService)
        {
            this.authService = authService;
        }

        public IActionResult Rent()
        {
            return View();
        }

        [Authorize(Policy = "Over18")]
        public IActionResult RentOver18()
        {
            return View();
        }

        public async Task<IActionResult> RentNewRelease(
            Rental inputModel)
        {
            var result = await authService.AuthorizeAsync(User, inputModel, "CanRentNewRelease");
            if (!result.Succeeded)
                return Challenge();

            return View();
        }
    }
}
