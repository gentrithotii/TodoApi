namespace ToDoApi.Models
{

    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }



    }
}

