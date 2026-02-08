using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Common.Helpers;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Models.Enums;
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
        private readonly IImageRepository imageRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository,
                                IMenuItemRepository menuItemRepository,
                                ISubMenuRepository subMenuRepository,
                                IMenuRepository menuRepository,
                                IImageRepository imageRepository)
        {
            this.restaurantRepository = restaurantRepository;
            this.menuItemRepository = menuItemRepository;
            this.subMenuRepository = subMenuRepository;
            this.menuRepository = menuRepository;
            this.imageRepository = imageRepository;
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

        public async Task<RestaurantResponseDto> GetAllRestaurantsAsync(string? menuName)
        {
            //Get Data from Database via Domain model
            List<Restaurant> restaurants;
            if (string.IsNullOrWhiteSpace(menuName))
            {
                restaurants = await restaurantRepository.GetAllAsync();
            }
            else
            {
                restaurants = await restaurantRepository.GetByMenuNameAsync(menuName);
            }

            var restaurantDtos = restaurants.Select(r => new RestaurantDto
            {
                Id = r.Id,
                name = r.name,
                rating = r.rating,
                is_open = r.is_open,
                created_at = (DateTime)r.created_at,
                updated_at = r.updated_at,
                RestaurantIconImage = r.RestaurantIcon == null
                ? null
                : new ImageDto
                {
                    Id = r.RestaurantIcon.Id,
                    FileName = r.RestaurantIcon.FileName,
                    FileExtension = r.RestaurantIcon.FileExtension,
                    FileSizeInBytes = r.RestaurantIcon.FileSizeInBytes,
                    FilePath = r.RestaurantIcon.FilePath,
                    ImageType = r.RestaurantIcon.ImageType,
                    created_at = (DateTime)r.RestaurantIcon.created_at,
                }
            }).ToList();

            return new RestaurantResponseDto
            {
                Restaurants = restaurantDtos
            };

        }
        public async Task<RestaurantMenuItemsDto> GetMenuItemsByRestaurantAsync(Guid restaurantId)
        {
            var menuItems = await menuItemRepository.GetAllMenuItemsAsync();
            var SubMenus = await subMenuRepository.GetAllSubMenusAsync();

            var filteredMenuItems = menuItems
            .Where(mi => mi.restaurantId == restaurantId)
            .ToList();

            var groupedMenuItems = filteredMenuItems
            .GroupBy(mi => mi.SubMenuId)
            .Select(group => new GroupedMenuItemsDto
            {
               SubMenuId = group.Key,
               SubMenuName = SubMenus.FirstOrDefault(sm => sm.Id == group.Key)?.Name,
               Items = group.Select(mi => new MenuItemsDto
               {
                    Id = mi.Id,
                    Name = mi.Name,
                    Description = mi.Description,
                    imageUrl = mi.imageUrl,
                    Price = mi.Price,
                    subMenuId = mi.SubMenuId,
                    is_Available = mi.is_Available,
                    restaurantId = mi.restaurantId

            }).ToList()
        }).ToList();
            return new RestaurantMenuItemsDto
            {
                RestaurantsMenuItems = groupedMenuItems
            };
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
                rating = restaurant.rating,
                is_open = restaurant.is_open,
                created_at = restaurant.created_at ?? DateTime.Now,
                updated_at = restaurant.updated_at,
                RestaurantIconImage = restaurant.RestaurantIcon == null
                ? null 
                : new ImageDto
                {
                    Id = restaurant.RestaurantIcon.Id,
                    FileName = restaurant.RestaurantIcon.FileName,
                    FileExtension = restaurant.RestaurantIcon.FileExtension,
                    FileSizeInBytes = restaurant.RestaurantIcon.FileSizeInBytes,
                    FilePath = restaurant.RestaurantIcon.FilePath,
                    ImageType = restaurant.RestaurantIcon.ImageType,
                    created_at = (DateTime)restaurant.RestaurantIcon.created_at,
                }   

            };
            return restaurantDto;
        }

        public async Task<RestaurantDto?> UpdateRestaurantAsync(Guid id, UpdateRestaurantRequestDto updateRestaurantRequestDto)
        {

            var restaurant = await restaurantRepository.GetRestaurantbyIdAsync(id);

            if (restaurant == null)
            {
                return null;
            }

            //Update fields
            restaurant.name = updateRestaurantRequestDto.name;
            restaurant.Address = updateRestaurantRequestDto.Address;
            restaurant.description = updateRestaurantRequestDto.description;
            restaurant.updated_at = DateTime.UtcNow;

            if (updateRestaurantRequestDto.RestaurantIconId.HasValue)
            {
                var image = await imageRepository.GetImageByIdAsync(updateRestaurantRequestDto.RestaurantIconId.Value);

                if (image == null){ throw new Exception("Image not found");  }

                if (image.ImageType != ImageType.RestaurantIcon) { throw new Exception("Image is not of type RestaurantIcon"); }
                image.RestaurantId = restaurant.Id;
                restaurant.RestaurantIcon = image;
                restaurant.RestaurantIconId = image.Id;

                //Assign the image to the restaurant
                restaurant.logo_url = image.FilePath;
                
            }

            //Persist the changes
            await restaurantRepository.UpdateRestaurantAsync(id, restaurant);
            
            var updatedRestaurant = await restaurantRepository.GetRestaurantbyIdAsync(id).ConfigureAwait(false);
            return RestaurantImageAssociationHelper.ToDto(updatedRestaurant);

        }
    }
}
