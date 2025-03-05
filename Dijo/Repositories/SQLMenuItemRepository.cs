using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Dijo.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Dijo.API.Repositories
{
    public class SQLMenuItemRepository: IMenuItemRepository
    {
        public readonly DijoDbContext dbContext;
        public SQLMenuItemRepository(DijoDbContext dijoDbContext) 
        {
            this.dbContext = dijoDbContext;
        }

        public async Task<MenuItem> CreateMenuItem(MenuItem item)
        {
           
            await dbContext.MenuItem.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<MenuItem?> DeleteMenuItem(Guid id)
        {
            var deletedItem = await dbContext.MenuItem.FirstOrDefaultAsync(x => x.Id == id);

            if (deletedItem == null) 
            {
                return null;    
            }

            dbContext.Remove(deletedItem);
            await dbContext.SaveChangesAsync();

            return deletedItem;
        }

        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            return await dbContext.MenuItem.ToListAsync();


        }

        public Task<MenuItem?> GetMenuItemByIdAsync(Guid id)
        {
            return dbContext.MenuItem.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MenuItem?> UpdateMenuItemAsync(Guid guid, MenuItem item)
        {
            var existingMenuItem = dbContext.MenuItem.FirstOrDefault(x => x.Id == guid);

            existingMenuItem.Name = item.Name;
            existingMenuItem.Description = item.Description;
            existingMenuItem.Price = item.Price;
            existingMenuItem.Image_url = item.Image_url;
            existingMenuItem.is_available = item.is_available;
            existingMenuItem.updated_at = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return existingMenuItem;
        }
    }
}
