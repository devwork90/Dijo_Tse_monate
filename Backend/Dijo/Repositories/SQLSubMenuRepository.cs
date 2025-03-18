using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.API.Repositories
{
    public class SQLSubMenuRepository: ISubMenuRepository
    {
        public readonly DijoDbContext dbContext;
        public SQLSubMenuRepository(DijoDbContext dijoDbContext) 
        {
            this.dbContext = dijoDbContext;
        }

        public async Task<SubMenu> CreateMenuItem(SubMenu item)
        {
           
            await dbContext.SubMenu.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<SubMenu?> DeleteMenuItem(Guid id)
        {
            var deletedItem = await dbContext.SubMenu.FirstOrDefaultAsync(x => x.Id == id);

            if (deletedItem == null) 
            {
                return null;    
            }

            dbContext.Remove(deletedItem);
            await dbContext.SaveChangesAsync();

            return deletedItem;
        }

        public async Task<List<SubMenu>> GetAllMenuItemsAsync()
        {
            return await dbContext.SubMenu.ToListAsync();


        }

        public Task<SubMenu?> GetMenuItemByIdAsync(Guid id)
        {
            return dbContext.SubMenu.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SubMenu?> UpdateMenuItemAsync(Guid guid, SubMenu item)
        {
            var existingMenuItem = dbContext.SubMenu.FirstOrDefault(x => x.Id == guid);

            if (existingMenuItem == null) 
            {
                return null;
            }

            existingMenuItem.Name = item.Name;
            existingMenuItem.is_available = item.is_available;
            existingMenuItem.updated_at = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return existingMenuItem;
        }
    }
}
