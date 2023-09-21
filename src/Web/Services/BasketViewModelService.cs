﻿using ApplicationCore.Entities;
using ApplicationCore.Services;
using System.Security.Claims;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderService _orderService;

        private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

        private string UserId => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        private string? AnonId => HttpContext.Request.Cookies[Constants.BASKET_COOKIE];

        private string BuyerId => UserId ?? AnonId ?? CreateAnonymousId();

        private string _createAnonId = null!;

        private string? CreateAnonymousId()
        {
            if (_createAnonId == null)
            {
                _createAnonId = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies.Append(Constants.BASKET_COOKIE, _createAnonId, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(14),
                    IsEssential = true

                });
            }
            return _createAnonId;
        }

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor, IOrderService orderService)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
        }

        public async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            var basket = await _basketService.GetOrCreateBasketAsync(BuyerId);
            return basket.ToBasketViewModel();
        }

        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity)
        {
            var basket = await _basketService.AddItemToBasketAsync(BuyerId, productId, quantity); 
            return basket.ToBasketViewModel();
        }

        public async Task EmptyBasketAsync()
        {
            await _basketService.EmptyBasketAsync(BuyerId);
        }
        public async Task RemoveItemAsync(int productId)
        {
            await _basketService.DeleteBasketItemAsync(BuyerId, productId);
        }

        public async Task<BasketViewModel> UpdateQuantitiesAsync(Dictionary<int, int> quantities)
        {
           var basket = await _basketService.SetQuantitiesAsync(BuyerId, quantities);
            return basket.ToBasketViewModel();
        }

        public async Task TransferBasketAsync()
        {
            if (AnonId == null || UserId == null)
                return;

            await _basketService.TransferBasketAsync(AnonId, UserId);
            HttpContext.Response.Cookies.Delete(Constants.BASKET_COOKIE);
        }

        public async Task CheckoutAsync(string street, string city, string? state, string country, string zip)
        {
            Address shippingAddres = new Address()
            {
                Street = street,
                City = city,
                State = state,
                Country = country,
                ZipCode = zip
            };

            await _orderService.CreateOrderAsync(UserId, shippingAddres);
            await _basketService.EmptyBasketAsync(BuyerId);
        }
    }
}
