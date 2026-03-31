using TodoTeams.Dto.Task;
using TodoTeams.Models;

namespace TodoTeams.Services.Interfaces
{
    public interface ITodoTaskService
    {
        Task<IEnumerable<TodoTaskDto>> GetAllAsync(TodoTaskStatus? status);
        Task<IEnumerable<TodoTaskDto>> GetByTeamIdAsync(int teamId);
        Task<IEnumerable<TodoTaskDto>> GetByUserIdAsync(int userId);
        Task<TodoTaskDto> GetByTaskIdAsync(int id);
        Task<TodoTaskDto> CreateAsync(CreateTodoTaskDto task);
        Task<TodoTaskDto> UpdateStatusAsync(int id, TodoTaskStatus newStatus);
        Task<TodoTaskDto> UpdateAssignedUserAsync(int id, int? assignedTo);
        Task<TodoTaskDto> UpdateTask(int id, UpdateTodoTaskDto update);
        Task<bool> DeleteByIdAsync(int id);
    }
}
