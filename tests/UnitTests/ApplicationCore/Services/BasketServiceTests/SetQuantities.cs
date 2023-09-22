using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class SetQuantities
    {
        // NSubstitute kütüphanesi ile lazım olan repoları taklit edebiliriz. 

        private readonly IRepository<Basket> _mockBasketRepo = Substitute.For<IRepository<Basket>>();
        private readonly IRepository<BasketItem> _mockBasketItemRepo = Substitute.For<IRepository<BasketItem>>();
        private readonly IRepository<Product> _mockProductRepo = Substitute.For<IRepository<Product>>();
        private readonly string _buyerId = "test-buyer-id";

        [Fact]
        public async Task ThrowsExceptionWhenQuantityIsNotPositive()
        {
            var basketService = new BasketService(_mockBasketRepo,_mockBasketItemRepo,_mockProductRepo);
            Dictionary<int,int> newQuantities = new Dictionary<int, int>() 
            {
                {1,3 },
                {2,0 },
                {4,1 }
            };

            // basketService.SetQuantities metodu negatif ya da 0 içeren bir quantity aldığında hata fırlatır.
            await Assert.ThrowsAsync<NonPositiveQuantityException>(async () => await basketService.SetQuantitiesAsync(_buyerId, newQuantities));
        }
    }
}
