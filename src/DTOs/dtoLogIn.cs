
using System.ComponentModel.DataAnnotations;


namespace Employee_Management_System_Backend.src.DTOs
{
    public class dtoLogIn
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}