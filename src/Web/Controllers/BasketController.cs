using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }

        [HttpPost]
        public async Task<ActionResult<BasketViewModel>> AddItem(int productId, int quantity = 1)
        {
            var basket = await _basketViewModelService.AddItemToBasketAsync(productId, quantity);
            return basket;
        }
        public async Task<IActionResult> Index()
        {
            var basket = await _basketViewModelService.GetBasketViewModelAsync();
            return View(basket);
        }

        [HttpPost]
        public async Task<IActionResult> EmptyBasket()
        {
            await _basketViewModelService.EmptyBasketAsync();
            TempData["SuccessMessageTemp"] = "Your basket is now empty.";
            return RedirectToAction("Index", "Basket");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            await _basketViewModelService.RemoveItemAsync(productId);
            TempData["SuccessMessageTemp"] = "Item successfully deleted from your basket.";
            return RedirectToAction("Index", "Basket");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([ModelBinder(Name = "quantities")] Dictionary<int, int> quantities)
        {
            await _basketViewModelService.UpdateQuantitiesAsync(quantities);
            TempData["SuccessMessageTemp"] = "Basket successfully updated.";
            return RedirectToAction("Index", "Basket");

        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketViewModelService.GetBasketViewModelAsync();
            var vm = new CheckoutViewModel()
            {
                Basket = basket
            };

            return View(vm);
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel vm)
        {
            if(ModelState.IsValid)
            {
                await _basketViewModelService.CheckoutAsync(vm.Street, vm.City, vm.State, vm.Country, vm.ZipCode);
                return RedirectToAction("OrderConfirmed");
            }

            vm.Basket = await _basketViewModelService.GetBasketViewModelAsync(); 
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> OrderConfirmed()
        {
            return View();
        }
    }
}
