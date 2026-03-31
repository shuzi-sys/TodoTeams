namespace TodoTeams.Dto.Team
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<TodoTeams.Dto.User.UserDto>? Users { get; set; }
    }
}
