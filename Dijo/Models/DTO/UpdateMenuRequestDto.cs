namespace Dijo.API.Models.DTO
{
    public class UpdateMenuRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool is_active { get; set; }
    }
}
