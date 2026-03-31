using TodoTeams.Models;

namespace TodoTeams.Dto.User
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int TeamId { get; set; }
        public ICollection<TodoTeams.Dto.Task.TodoTaskDto> AssignedTasks { get; set; }
    }
}
