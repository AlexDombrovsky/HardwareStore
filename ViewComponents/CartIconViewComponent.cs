using System.Threading.Tasks;
using HardwareStore.Interfaces;
using HardwareStore.Models.Cart;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.ViewComponents
{
    public class CartIconViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<IdentityUser> _userManager;

        public CartIconViewComponent(IOrderService orderService, UserManager<IdentityUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CartIconViewModel
            {
                ProductsCount = await GetOrdersCount(),
                Total = GetTotalSum()
            };

            return View(model);
        }

        private async Task<int> GetOrdersCount()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return await _orderService.GetOrdersCountByUser(userId);
        }

        private int GetTotalSum()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return _orderService.GetTotalSum(userId);
        }
    }
}