using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoTeams.Services;
using TodoTeams.Models;
using TodoTeams.Services.Interfaces;
using TodoTeams.Dto.Team;

namespace TodoTeams.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService) { _teamService = teamService; }
        // GET: TeamController

        [HttpGet]
        public async Task<IActionResult> GetTeamByIdAsync(int id) {
            var team = await _teamService.GetTeamAsync(id); 
            if (team == null) { return NotFound(); }
            return Ok(team);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeamsAsync() {
            ICollection<TeamDto> Teams = await _teamService.GetAllTeamsAsync();
            if (Teams != null && Teams.Any()) {  return Ok(Teams); }
            return NotFound();
        }

        [HttpDelete]
        public async Task DeleteTeamAsync(int id) { await _teamService.DeleteTeam(id); }

        [HttpPatch]
        public async Task UpdateTeamAsync(int id, UpdateTeamDto Dto) { await _teamService.UpdateTeam(id, Dto); }
    }
}
