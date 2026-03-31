namespace TodoTeams.Dto.Task
{
    public class UpdateStatusTodoTaskDto
    {
        public int Id { get; set; }
        public TodoTeams.Models.TodoTaskStatus Status { get; set; }
    }
}
