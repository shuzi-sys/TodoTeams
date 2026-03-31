using TodoTeams.Models;

namespace TodoTeams.Dto.Task
{
    public class CreateTodoTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoTeams.Models.User AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
