using Microsoft.AspNetCore.Mvc;

namespace Web.View_Components
{
    public class NavbarBasketViewComponent : ViewComponent
    {
        private readonly IBasketViewModelService _basketViewModelService;

        public NavbarBasketViewComponent(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // login olunmuşsa kullanıcı ıd, olunmamışsa cookielerde saklanan anonymusId eğer o da yoksa şimdi üretilecek ve sonra cookielerde saklanacak bir benzersiz anonymusId parametre olarak sokulmalı.

            var basket = await _basketViewModelService.GetBasketViewModelAsync();
            return View(basket);
        }
    }
}
