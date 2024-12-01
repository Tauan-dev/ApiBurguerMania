using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBurguerMania.Data;
using ApiBurguerMania.Dto.Status;
using ApiBurguerMania.Models;
using ApiBurguerMania.Service.Interface;

namespace ApiBurguerMania.Service
{
    public class StatusService : IStatusService
    {
        private readonly ApplicationDbContext _dbContext;

        public StatusService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obter todos os status
        public async Task<IEnumerable<StatusDTO>> GetAllStatusAsync()
        {
            return await _dbContext.Statuses
                .Select(status => new StatusDTO
                {
                    Id = status.Id,  // Usando "Id" conforme o DTO
                    Name = status.Name
                })
                .ToListAsync();
        }

        // Obter um status por ID
        public async Task<StatusDTO> GetStatusByIdAsync(int statusId)
        {
            var status = await _dbContext.Statuses
                .FirstOrDefaultAsync(s => s.Id == statusId);  // Alterado para "Id"

            if (status == null)
            {
                throw new KeyNotFoundException("Status não encontrado");
            }

            return new StatusDTO
            {
                Id = status.Id,  // Usando "Id" conforme o DTO
                Name = status.Name
            };
        }

        // Criar um novo status
        public async Task<StatusDTO> CreateStatusAsync(StatusDTO statusDto)
        {
            if (statusDto == null)
            {
                throw new ArgumentNullException(nameof(statusDto), "Os dados do status não podem ser nulos.");
            }

            var status = new Status
            {
                Name = statusDto.Name
            };

            _dbContext.Statuses.Add(status);
            await _dbContext.SaveChangesAsync();

            return new StatusDTO
            {
                Id = status.Id,  // Usando "Id" conforme o DTO
                Name = status.Name
            };
        }

        // Atualizar um status existente
        public async Task<bool> UpdateStatusAsync(int statusId, StatusDTO statusDto)
        {
            if (statusDto == null)
            {
                throw new ArgumentNullException(nameof(statusDto), "Os dados do status não podem ser nulos.");
            }

            var status = await _dbContext.Statuses.FindAsync(statusId);
            if (status == null)
            {
                throw new KeyNotFoundException("Status não encontrado");
            }

            status.Name = statusDto.Name;

            _dbContext.Entry(status).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Deletar um status
        public async Task<bool> DeleteStatusAsync(int statusId)
        {
            var status = await _dbContext.Statuses.FindAsync(statusId);
            if (status == null)
            {
                throw new KeyNotFoundException("Status não encontrado");
            }

            _dbContext.Statuses.Remove(status);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
