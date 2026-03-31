using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoTeams.Data;
using TodoTeams.Dto.Team;
using TodoTeams.Dto.User;
using TodoTeams.Services.Interfaces;

namespace TodoTeams.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _db;
        public TeamService(AppDbContext _DbContext) { _db = _DbContext; }

        public async Task<TeamDto> GetTeamAsync(int id)
        {
            var team = await _db.Teams.Include(t=> t.Users).FirstOrDefaultAsync(t=> t.Id == id);
            if (team == null) return null;
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                CreationDate = team.CreationDate,
                Users = team.Users.Select(u => new UserDto
                {
                    Name = u.Name,
                    Email = u.Email
                }).ToList()
            };
        }

        public async Task<List<TeamDto>> GetAllTeamsAsync()
        {
            return await _db.Teams.Include(t => t.Users).ToListAsync();
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _db.Teams.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) return false;
            foreach (var user in team.Users) {
                user.Team = null;
            }
            _db.Remove(team);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TeamDto> UpdateTeamAsync(int id, TeamDto update) {
            var team = await _db.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) { return null; }
            if (update.Name!=null) { team.Name = update.Name; }
            await _db.SaveChangesAsync();
            return update;
        }
    }
}
