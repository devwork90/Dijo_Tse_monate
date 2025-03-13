namespace RestaurantAPI.Service
{
    public interface ISubMenuSerice
    {
        Task  DeleteSubMenusByMenuIdAsync(Guid guid);
        //Task DeleteSubMenusByRestaurantIdAsync(Guid restaurantId);
    }
}
