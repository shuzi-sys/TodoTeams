namespace TodoTeams.Dto.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int TeamId { get; set; }
    }
}
