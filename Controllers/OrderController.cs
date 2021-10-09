using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HardwareStore.Data.Entities.Products;
using HardwareStore.Interfaces;
using HardwareStore.Models.Cart;
using HardwareStore.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<IdentityUser> userManager, IMapper mapper, SignInManager<IdentityUser> signInManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var orders = await _orderService.GetOrdersByUserId(user.Id);
            var model = new CartViewModel
            {
                Orders = _mapper.Map<List<OrderViewModel>>(orders),
                Total = GetTotalSum()
            };
            return View(model);
        }

        public async Task<IActionResult> AddToCart(int id, string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                await _orderService.Create(new Order
                {
                    UserId = user.Id,
                    ProductId = id
                });
            }

            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await _orderService.Delete(id);
            return RedirectToAction("Index");
        }

        private int GetTotalSum()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return _orderService.GetTotalSum(userId);
        }
    }
}