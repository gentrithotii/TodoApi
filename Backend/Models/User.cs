using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(200)]
        public required string PasswordHash { get; set; }
        public List<ToDoItem> ToDoItems { get; set; } = [];
    }
}