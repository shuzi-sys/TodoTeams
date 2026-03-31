using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TodoTeams.Data;
using TodoTeams.Dto.Task;
using TodoTeams.Models;
using TodoTeams.Services.Interfaces;
namespace TodoTeams.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        public TodoTaskService(AppDbContext db) { _db = db; }
        private readonly AppDbContext _db;

        public async Task<IEnumerable<TodoTaskDto>> GetAllAsync(TodoTaskStatus? sentstatus)
        {
            var query = _db.Tasks.Include(t => t.AssignedTo).AsQueryable();
            if (sentstatus != null)
            {
                query = query.Where(t => t.Status == sentstatus.Value);
            }
            return await query.Select(t => new TodoTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                AssignedTo = new AssignedToDto { Id = t.AssignedTo.Id, Name = t.AssignedTo.Name }
            }).ToListAsync();
        }

        public async Task<IEnumerable<TodoTaskDto>> GetByTeamIdAsync(int id)
        {
            var team = await _db.Teams.Include(t => t.Users).ThenInclude(u => u.AssignedTasks).ThenInclude(t => t.AssignedTo).FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) { return null; }

            return team.Users.SelectMany(u => u.AssignedTasks).Select(task => new TodoTaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    AssignedTo = new AssignedToDto
                    {
                        Id = task.AssignedTo.Id,
                        Name = task.AssignedTo.Name
                    }
                }).ToList();
        }

        public async Task<IEnumerable<TodoTaskDto>> GetByUserIdAsync(int userId)
        {
            var tasks = await _db.Tasks.Where(t => t.AssignedTo.Id == userId).Select(
                task => new TodoTaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    AssignedTo = new AssignedToDto
                    {
                        Id = task.AssignedTo.Id,
                        Name = task.AssignedTo.Name,
                    }
                }
                ).ToListAsync();
            return tasks;
        }

        public async Task<TodoTaskDto> GetByTaskIdAsync(int taskId)
        {
            return await _db.Tasks.Include(t => t.AssignedTo).Where(_ => _.Id == taskId).Select(
                task => new TodoTaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    AssignedTo = new AssignedToDto
                    {
                        Id = task.AssignedTo.Id,
                        Name = task.AssignedTo.Name,
                    }
                }).FirstOrDefaultAsync();
        }

        public async Task<TodoTaskDto> CreateAsync(CreateTodoTaskDto submittedtask)
        {
            var user = await _db.Users.FindAsync(submittedtask.AssignedTo.Id);
            var task = new TodoTeams.Models.TodoTask
            {
                AssignedTo = user,
                Title = submittedtask.Title,
                Description = submittedtask.Description,
                DueDate = submittedtask.DueDate,
                Status = TodoTaskStatus.Pending,
                CreatedAt = DateTime.UtcNow,
            };
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            return new TodoTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                AssignedTo = new AssignedToDto
                {
                    Id = user.Id,
                    Name = user.Name
                }
            };
        }

        public async Task<TodoTaskDto> UpdateStatusAsync(int taskId, TodoTaskStatus newStatus)
        {
            var task = await _db.Tasks.Include(t => t.AssignedTo).FirstOrDefaultAsync(t => t.Id == taskId);
            task.Status = newStatus;
            await _db.SaveChangesAsync();

            return new TodoTaskDto
            {
                Id = taskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                AssignedTo = new AssignedToDto { Id = task.AssignedTo.Id, Name = task.AssignedTo.Name },
            };
        }

        public async Task<TodoTaskDto> UpdateAssignedUserAsync(int taskId, int? Assignedto)
        {
            var task = await _db.Tasks.Include(t => t.AssignedTo).FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null) { return null; }
            var user = await _db.Users.FindAsync(Assignedto.Value);
            if (user == null) { return null; }
            task.AssignedTo = user;

            await _db.SaveChangesAsync();
            return new TodoTaskDto
            {
                Id = taskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                AssignedTo = new AssignedToDto
                {
                    Id = task.AssignedTo.Id,
                    Name = task.AssignedTo.Name,
                }

            };
        }

        public async Task<TodoTaskDto> UpdateTask(int id, UpdateTodoTaskDto update)
        {
            var task = await _db.Tasks.Include(t => t.AssignedTo).FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) { return null; }
            if (update.Title != null) { task.Title = update.Title; }
            if (update.Description != null) { task.Description = update.Description; }
            if (update.Status != null) { task.Status = update.Status.Value; }
            if (update.DueDate != null) { task.DueDate = update.DueDate; }
            await _db.SaveChangesAsync();
            return new TodoTaskDto
            {
                Id = id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                AssignedTo = new AssignedToDto
                {
                    Id = task.AssignedTo.Id,
                    Name = task.AssignedTo.Name,
                }
            };
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) { return false; }
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
