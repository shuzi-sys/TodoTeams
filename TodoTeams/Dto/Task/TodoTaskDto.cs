namespace TodoTeams.Dto.Task
{
    public class TodoTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoTeams.Models.TodoTaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AssignedToDto AssignedTo { get; set; }
    }
}
