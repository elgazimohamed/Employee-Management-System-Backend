
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System_Backend.src.DTOs
{
    public class dtoNewUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

    }
}