using TodoTeams.Models;
using TodoTeams.Dto.Team;
namespace TodoTeams.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> GetTeamAsync(int id);
        Task<List<TeamDto>> GetAllTeamsAsync();
        Task<bool> DeleteTeamAsync(int id);
        Task<TeamDto> UpdateTeamAsync(int id, TeamDto update);
    }
}
