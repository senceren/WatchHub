using Microsoft.AspNetCore.Mvc;

namespace Web.View_Components
{
    public class NavbarBasketViewComponent : ViewComponent
    {

        public NavbarBasketViewComponent()
        {
           
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // login olunmuşsa kullanıcı ıd, olunmamışsa cookielerde saklanan anonymusId eğer o da yoksa şimdi üretilecek ve sonra cookielerde saklanacak bir benzersiz anonymusId parametre olarak sokulmalı.
            return View();
        }
    }
}
