using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Repositories;
using System.Linq;
namespace RestaurantAPI.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly ISubMenuRepository subMenuRepository;
        private readonly IMenuRepository menuRepository;
        private readonly IMenuItemRepository menuItemRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository,
                                IMenuItemRepository menuItemRepository,
                                ISubMenuRepository subMenuRepository,
                                IMenuRepository menuRepository)
        {
            this.restaurantRepository = restaurantRepository;
            this.menuItemRepository = menuItemRepository;
            this.subMenuRepository = subMenuRepository;
            this.menuRepository = menuRepository;
        }

        public async Task<RestaurantDto> CreateRestaurantAsync(AddRestaurantRequestDto addRestaurantRequestDto)

        {
            //Map DTO to Domain model
            var restaurantDomainModel = new Restaurant
            {
                name = addRestaurantRequestDto.name,
                Address = addRestaurantRequestDto.Address,
                description = addRestaurantRequestDto.description,
                logo_url = addRestaurantRequestDto.logo_url,
                rating = addRestaurantRequestDto.rating,
                is_open = addRestaurantRequestDto.is_open,
                created_at = DateTime.UtcNow

            };

            //Use domain model to ceate a restaurant in the DB
            await restaurantRepository.CreateRestaurantAsync(restaurantDomainModel);


            //Map Domain models back to DTOs
            var restaurantDto = new RestaurantDto
            {
                Id = restaurantDomainModel.Id,
                name = restaurantDomainModel.name,
                Address = restaurantDomainModel.Address,
                description = restaurantDomainModel.description,
                logo_url = restaurantDomainModel.logo_url,
                rating = restaurantDomainModel.rating,
                created_at = (DateTime)restaurantDomainModel.created_at,
                is_open = restaurantDomainModel.is_open,
            };

            return restaurantDto;
        }

        public async Task<bool> DeleteRestaurantAsync(Guid id)
        {
            //Get data from Database via - Domain models
            var restaurant = await restaurantRepository.DeleteRestaurantAsync(id);
            if (restaurant == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<RestaurantResponseDto>> GetAllRestaurantsAsync(string? menuName)
        {
            //Get Data from Database via Domain model
            var restaurants = await restaurantRepository.GetAllAsync(menuName);
            var Menus = await menuRepository.GetAllMenusAsync();
            var SubMenus = await subMenuRepository.GetAllSubMenusAsync();
            var MenuItems = await menuItemRepository.GetAllMenuItemsAsync();

            if(menuName == null)
            {
                //Map Domain modela to DTOs
                var restaurantDto = new List<RestaurantDto>();
                foreach (var restaurant in restaurants)
                {
                    restaurantDto.Add(new RestaurantDto
                    {
                        Id = restaurant.Id,
                        name = restaurant.name,
                        Address = restaurant.Address,
                        description = restaurant.description,
                        logo_url = restaurant.logo_url,
                        rating = restaurant.rating,
                        is_open = restaurant.is_open,
                        created_at = restaurant.created_at ?? DateTime.Now,
                        updated_at = restaurant.updated_at,

                    });
                }

                //return DTOs back to client
                return new List<RestaurantResponseDto> { new RestaurantResponseDto { Restaurants = restaurantDto } };
            }
            else
            {
                var result = restaurants
                .Join(SubMenus,
                  r => r.Id,
                  sb => sb.restaurantId,
                  (r, sb) => new { r, sb })
                .Join(Menus,
                  rs => rs.sb.MenuId,
                   m => m.Id,
                   (rs, m) => new { rs.r, rs.sb, m })
                .Where(rsm => rsm.m.Name == menuName)
                .GroupBy(rsm => new { rsm.r.Id, rsm.r.name })
                .Select(g => new RestaurantCategoryDto
                {
                    Id = g.Key.Id,
                    RestaurantName = g.Key.name,
                    MenuName = g.Min(x => x.m.Name),
                    SubMenuName = g.Min(x => x.sb.Name),
                    SubMenuId = g.Min(x => x.sb.Id)
                }).ToList();

                return new List<RestaurantResponseDto> { new RestaurantResponseDto { Categories = result } };
            }
        }

        public async Task<List<MenuItemsDto>> GetMenuItemsByRestaurantAsync(Guid restaurantId)
        {
            var MenuItems = await menuItemRepository.GetAllMenuItemsAsync();
            var SubMenus = await subMenuRepository.GetAllSubMenusAsync();
            var returnedMenuItems = MenuItems
                .Where(mi => mi.restaurantId == restaurantId)
                .Join( SubMenus,
                mi => mi.SubMenuId,
                sb => sb.Id,
                (mi, sb) => new MenuItemsDto
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    Description = mi.Description,
                    imageUrl = mi.imageUrl,
                    Price = mi.Price,
                    subMenuId = mi.SubMenuId,
                    is_Available = mi.is_Available,
                    restaurantId = mi.restaurantId,


                })
                .ToList();
            return returnedMenuItems;
        }

        public async Task<RestaurantDto?> GetRestaurantbyIdAsync(Guid id)
        {
            //Get data from Database via - Domain models
            var restaurant = await restaurantRepository.GetRestaurantbyIdAsync(id);

            if (restaurant == null)
            {
                return null;
            }

            //Map Domain models to DTO

            var restaurantDto = new RestaurantDto
            {
                Id = restaurant.Id,
                name = restaurant.name,
                Address = restaurant.Address,
                description = restaurant.description,
                logo_url = restaurant.logo_url,
                rating = restaurant.rating,
                is_open = restaurant.is_open,
                created_at = restaurant.created_at ?? DateTime.Now,
                updated_at = restaurant.updated_at,

            };
            return restaurantDto;
        }

        public async Task<RestaurantDto?> UpdateRestaurantAsync(Guid id, UpdateRestaurantRequestDto updateRestaurantRequestDto)
        {

                //Map DTO to Domain Model
                var restaurantDomainModel = new Restaurant
                {
                    name = updateRestaurantRequestDto.name,
                    Address = updateRestaurantRequestDto.Address,
                    description = updateRestaurantRequestDto.description,
                    logo_url = updateRestaurantRequestDto.logo_url,
                    rating = updateRestaurantRequestDto.rating,
                    is_open = updateRestaurantRequestDto.is_open,
                };

                //Check if the restaurant exists
                restaurantDomainModel = await restaurantRepository.UpdateRestaurantAsync(id, restaurantDomainModel);

                if (restaurantDomainModel == null)
                {
                    return null;
                }

                //Convert Domain Model to DTO

                var restaurantDto = new RestaurantDto
                {
                    Id = restaurantDomainModel.Id,
                    name = restaurantDomainModel.name,
                    Address = restaurantDomainModel.Address,
                    description = restaurantDomainModel.description,
                    logo_url = restaurantDomainModel.logo_url,
                    rating = restaurantDomainModel.rating,
                    is_open = restaurantDomainModel.is_open,
                    created_at = (DateTime)restaurantDomainModel.created_at,
                    updated_at = restaurantDomainModel.updated_at,

                };
              return restaurantDto;
        }
    }
}
