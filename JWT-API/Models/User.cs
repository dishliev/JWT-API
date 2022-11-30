using System.ComponentModel.DataAnnotations;

namespace JWT_API.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
