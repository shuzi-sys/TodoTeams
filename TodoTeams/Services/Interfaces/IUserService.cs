using TodoTeams.Dto.User;
namespace TodoTeams.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);
        Task<UserDto> UpdateUser(UpdateUserDto dto, int id);
        Task<bool> DeleteUser(int id);
    }
}
