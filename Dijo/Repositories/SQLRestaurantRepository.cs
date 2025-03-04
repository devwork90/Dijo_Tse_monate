using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Dijo.API.Repositories
{
    public class SQLRestaurantRepository: IRestaurantRepository
    {
        private readonly DijoDbContext dbContext;

        public SQLRestaurantRepository(DijoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant)
        {
            await dbContext.restaurants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();

            return restaurant;
        }

        public async Task<Restaurant?> DeleteRestaurantAsync(Guid id)
        {
            var deletedRestaurant = await dbContext.restaurants.FirstOrDefaultAsync(x => x.Id == id);

            if (deletedRestaurant == null) 
            {
                return null;
            }

            dbContext.Remove(deletedRestaurant);
            await dbContext.SaveChangesAsync();

            return deletedRestaurant;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await dbContext.restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantbyIdAsync(Guid id)
        {
            //Get data from Database via - Domain models
           return await dbContext.restaurants.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Restaurant?> UpdateRestaurantAsync(Guid id, Restaurant restaurant)
        {
            var existingRestaurant = await dbContext.restaurants.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRestaurant == null)
            {
                return null;
            }

            existingRestaurant.name = restaurant.name;
            existingRestaurant.Address = restaurant.Address;
            existingRestaurant.description = restaurant.description;
            existingRestaurant.logo_url = restaurant.logo_url;
            existingRestaurant.rating = restaurant.rating;
            existingRestaurant.is_open = restaurant.is_open;
            existingRestaurant.updated_at = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return existingRestaurant;
        }
    }
}
