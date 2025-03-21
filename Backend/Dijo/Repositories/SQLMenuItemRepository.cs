using Microsoft.EntityFrameworkCore;
using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;

namespace RestaurantAPI.Repositories
{
    public class SQLMenuItemRepository : IMenuItemRepository
    {
        private readonly DijoDbContext dbContext;

        public SQLMenuItemRepository(DijoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<MenuItem> CreateMenuItem(MenuItem menuItem)
        {
            //Use Domain Model to create a new MenuItem in database
            await dbContext.MenuItems.AddAsync(menuItem);
            await dbContext.SaveChangesAsync();

            return menuItem;
        }

        public async Task<MenuItem?> DeleteMenuItem(Guid id)
        {
            var existingMenuItem = await dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (existingMenuItem == null)
            {
                return null;

            }
            dbContext.MenuItems.Remove(existingMenuItem);
            await dbContext.SaveChangesAsync();
            return existingMenuItem;
        }

        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            //Get Data from the Database through the dbContext
           return await dbContext.MenuItems.ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(Guid id)
        {
            //Get Data from the Database through the dbContext
            var menuItem = await dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (menuItem == null)
            {
                return null;
            }

            return menuItem;
        }

        public async Task<MenuItem?> UpdateMenuItem(Guid id, MenuItem menuItem)
        {
            // Get Data from the Database through the dbContext
            var existingMenuItem = await dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            //Check if the item exists
            if (existingMenuItem == null)
            {
                return null;
            }

            existingMenuItem.Name = menuItem.Name;
            existingMenuItem.Description = menuItem.Description;
            existingMenuItem.imageUrl = menuItem.imageUrl;
            existingMenuItem.is_Available = menuItem.is_Available;
            existingMenuItem.Price = menuItem.Price;
            existingMenuItem.updated_at = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return existingMenuItem;
        }
    }
}