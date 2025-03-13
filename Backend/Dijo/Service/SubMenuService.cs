
using RestaurantAPI.API.Repositories;

namespace RestaurantAPI.Service
{
    public class SubMenuService : ISubMenuSerice
    {
        private readonly ISubMenuRepository subMenuRepository;
        //private readonly IRestaurantRepository _restaurantRepository;

        public SubMenuService(ISubMenuRepository subMenuRepository) 
        {
            this.subMenuRepository = subMenuRepository;
            
        }
        public async Task DeleteSubMenusByMenuIdAsync(Guid id)
        {
            var SubMenus = await subMenuRepository.GetMenuItemByIdAsync(id);

           if (SubMenus == null) { return; }
            

            await subMenuRepository.DeleteMenuItem(id);
        }

        //public async Task DeleteSubMenusByRestaurantIdAsync(Guid id)
        //{
        //    var SubMenus = await _restaurantRepository.GetRestaurantbyIdAsync(id);

        //    if (SubMenus == null) { return; }

        //    await _restaurantRepository.DeleteRestaurantAsync(id);
        //}
    }
}
