using ApplicationCore.Entities;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;

        public HomeViewModelService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var vm = new HomeViewModel()
            {
                Products = (await _productRepo.GetAllAsync()).Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureUri = x.PictureUri,
                    Price = x.Price
                }).ToList()
            };

            return vm;
        }
    }
}
