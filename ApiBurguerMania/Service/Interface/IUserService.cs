using ApiBurguerMania.Dto.User;

namespace ApiBurguerMania.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<UserDTO> CreateUserAsync(CreateUserDTO userDto);
        Task<bool> UpdateUserAsync(int userId, UpdateUserDTO userDto);
        Task<bool> DeleteUserAsync(int userId);
    }
}
