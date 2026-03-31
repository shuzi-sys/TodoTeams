namespace TodoTeams.Dto.Team
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public ICollection<TodoTeams.Models.User>? Members { get; set; }
    }
}
