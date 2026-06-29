using OrderAPI.Models.DTO;

namespace OrderAPI.Service
{
    public class RestaurantService: IRestaurantService
    {
        private readonly HttpClient? _httpClient;

        public RestaurantService(HttpClient? _httpClient)
        {
            this._httpClient = _httpClient;
        }

        public async Task<MenuItemDTO?> GetMenuItemAsync(Guid menuItemId)
        {
            return await _httpClient.GetFromJsonAsync<MenuItemDTO>(
                $"api/MenuItem/{menuItemId}");
        }
    }
}
