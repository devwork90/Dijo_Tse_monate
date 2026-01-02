using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.API.Repositories
{
    public class SQLMenuRepository: IMenuRepository
    {
       private readonly DijoDbContext dbContext;
    

    public SQLMenuRepository(DijoDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            //Use domain model to create a new menu item in the DB
            await dbContext.Menu.AddAsync(menu);
            await dbContext.SaveChangesAsync();

            return menu;
        }

        public async Task<Menu?> DeleteMenuAsync(Guid id)
        {
            var deletedMenu = await dbContext.Menu
                .Include(m => m.SubMenus)
                //.Include(m => m.Restaurants)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (deletedMenu == null) 
            {
                return null; 
            }
            dbContext.Remove(deletedMenu);

            // Remove the Menu (which deletes its SubMenus due to cascade delete)
            await dbContext.SaveChangesAsync();

            return deletedMenu;
        }

        public async Task<List<Menu>> GetAllMenusAsync()
        {
            return await dbContext.Menu
                .Include(m => m.MenuIconImage)
                .ToListAsync();
        }

        public Task<Menu?> GetByIdAsync(Guid id)
        {
            return dbContext.Menu
                .Include(m => m.MenuIconImage)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Menu?> UpdateMenuAsync(Guid id, Menu menu)
        {
            var existingMenu = dbContext.Menu.FirstOrDefault(x => x.Id == id);  

            if (existingMenu == null)
            {
                return null;
            }

            existingMenu.Name = menu.Name;
            existingMenu.Description = menu.Description;
            existingMenu.is_active = menu.is_active;
            existingMenu.updated_at = DateTime.UtcNow;
            existingMenu.url_menu_icon = menu.url_menu_icon;

            await dbContext.SaveChangesAsync();

            return existingMenu;
        }
    }
}
