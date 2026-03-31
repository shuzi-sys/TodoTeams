namespace TodoTeams.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Team Team { get; set; }
        public ICollection<TodoTask> AssignedTasks { get; set; }
    }
}
