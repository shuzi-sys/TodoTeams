using Microsoft.EntityFrameworkCore;
using TodoTeams.Data;
using TodoTeams.Dto.Task;
using TodoTeams.Dto.User;
using TodoTeams.Services.Interfaces;

namespace TodoTeams.Services
{
    public class UserService : IUserService
    {
        public UserService(AppDbContext db) { _db = db; }
        private readonly AppDbContext _db;

        public async Task<UserDto> GetById(int id)
        {
            var user = await _db.Users.Include(t=> t.AssignedTasks).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) { return null; }
            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                TeamId = user.Team.Id,
                AssignedTasks = user.AssignedTasks.Select(task => new TodoTaskDto
                {
                    Id = task.Id,
                    Title = task.Title
                }).ToList()
            };
        }

        public async Task<UpdateUserDto> UpdateUser(UpdateUserDto dto, int id) 
        { 
         var user = await _db.Users.FirstOrDefaultAsync(u=> u.Id == id);
            if(user == null) { return null; }
            if (dto.Name != null) { user.Name = dto.Name; }
            if (dto.Email != null) { user.Email = dto.Email; }
            await _db.SaveChangesAsync();
            return new UpdateUserDto { Name = user.Name, Email = user.Email };
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) { return false; }
            _db.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }
}
