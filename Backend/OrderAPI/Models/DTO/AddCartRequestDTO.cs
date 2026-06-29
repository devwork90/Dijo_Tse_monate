namespace OrderAPI.Models.DTO
{
    public class AddCartRequestDTO
    {
        public Guid UserId { get; set; }
       public Guid RestaurantId { get; set; }

    }
}
