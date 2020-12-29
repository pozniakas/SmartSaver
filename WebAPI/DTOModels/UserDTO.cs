using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOModels
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
