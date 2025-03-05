using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dijo.API.Repositories
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
            var deletedMenu = dbContext.Menu.FirstOrDefault(x => x.Id == id);
            
            if (deletedMenu == null) 
            {
                return null;   
            }

            dbContext.Remove(deletedMenu);
            await dbContext.SaveChangesAsync();

            return deletedMenu;
        }

        public async Task<List<Menu>> GetAllMenusAsync()
        {
            return await dbContext.Menu.ToListAsync();
        }

        public Task<Menu?> GetByIdAsync(Guid id)
        {
            return dbContext.Menu.FirstOrDefaultAsync(x => x.Id == id);

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

            await dbContext.SaveChangesAsync();

            return existingMenu;
        }
    }
}
