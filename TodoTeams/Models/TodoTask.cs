namespace TodoTeams.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoTaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public User AssignedTo { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }

    public enum TodoTaskStatus {Pending, InProgress, Done}
    public class Tag 
    {
        public int Id { get; set; }
        public string Name { get; set; } // Urgent, Bug, Feature
        public ICollection<TodoTask> Tasks { get; set; }
    }
}
