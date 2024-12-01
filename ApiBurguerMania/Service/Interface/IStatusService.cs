using ApiBurguerMania.Dto.Status;

namespace ApiBurguerMania.Service.Interface
{
    public interface IStatusService
    {
        // Método para obter todos os Status
        Task<IEnumerable<StatusDTO>> GetAllStatusAsync();

        // Método para obter um Status por ID
        Task<StatusDTO> GetStatusByIdAsync(int statusId);

        // Método para criar um novo Status
        Task<StatusDTO> CreateStatusAsync(StatusDTO statusDto);

        // Método para atualizar um Status existente
        Task<bool> UpdateStatusAsync(int statusId, StatusDTO statusDto);

        // Método para deletar um Status
        Task<bool> DeleteStatusAsync(int statusId);
    }
}
