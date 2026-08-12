using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Models.DTO
{
    public class UserRegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phonenumber { get; set; }

        public Guid? RestaurantId { get; set; }

        public string[]? Roles { get; set; }
    }
}
