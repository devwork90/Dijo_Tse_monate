namespace RestaurantAPI.Service
{
    public interface ISubMenuSerice
    {
        void DeleteAmenu(Guid guid);
        //Task DeleteSubMenusByRestaurantIdAsync(Guid restaurantId);
    }
}
