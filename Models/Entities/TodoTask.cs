namespace todo_list_backend.Models.Entities
{
    public class TodoTask
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public long Index { get; set; }
        public string Priority { get; set; }
        public string List { get; set; }
        public string Email { get; set; }
        public string DueDate { get; set; }
    }
}