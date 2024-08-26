using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class UserDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(200)]
        public required string Password { get; set; }
        public List<ToDoItem> ToDoItems { get; set; } = [];
    }
}