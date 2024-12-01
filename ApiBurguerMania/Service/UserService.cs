using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBurguerMania.Data;
using ApiBurguerMania.Dto.User;
using ApiBurguerMania.Models;
using ApiBurguerMania.Service.Interface;

namespace ApiBurguerMania.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obter todos os usuários
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                })
                .ToListAsync();
        }

        // Obter um usuário por ID
        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }

        // Criar um novo usuário
        public async Task<UserDTO> CreateUserAsync(CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "Os dados do usuário não podem ser nulos.");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password, // Mapeando a senha
                
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }

        // Atualizar um usuário existente
        public async Task<bool> UpdateUserAsync(int userId, UpdateUserDTO userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "Os dados do usuário não podem ser nulos.");
            }

            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password; // Atualizando a senha

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Deletar um usuário
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
