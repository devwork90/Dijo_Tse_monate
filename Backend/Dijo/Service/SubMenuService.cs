
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.API.Data;
using RestaurantAPI.API.Repositories;

namespace RestaurantAPI.Service
{
    public class SubMenuService : ISubMenuSerice
    {
        private readonly IMenuRepository menuRepository;
        //private readonly IRestaurantRepository _restaurantRepository;

        public SubMenuService(IMenuRepository menuRepository) 
        {
            this.menuRepository = menuRepository;
            
        }
        public void DeleteAmenu(Guid id)
        {
            menuRepository.GetByIdAsync(id);
        }

        //public async Task DeleteSubMenusByRestaurantIdAsync(Guid id)
        //{
        //    var SubMenus = await _restaurantRepository.GetRestaurantbyIdAsync(id);

        //    if (SubMenus == null) { return; }

        //    await _restaurantRepository.DeleteRestaurantAsync(id);
        //}
    }
}
