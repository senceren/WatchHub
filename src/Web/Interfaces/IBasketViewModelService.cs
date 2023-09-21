using ApplicationCore.Entities;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetBasketViewModelAsync();

        Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity);

        Task EmptyBasketAsync();

        Task RemoveItemAsync(int productId);

        Task<BasketViewModel> UpdateQuantitiesAsync(Dictionary<int, int> quantities);
        Task TransferBasketAsync();

        Task CheckoutAsync(string street, string city, string? state, string country, string zip);
    }
}
