namespace TodoTeams.Dto.Team
{
    public class UpdateTeamDto
    {
        public string? Name { get; set; }
        public ICollection<int>? MembersToAdd { get; set; }
        public ICollection<int>? MembersToRemove { get; set; }
    }
}
