using ApiBurguerMania.Models;

namespace ApiBurguerMania.Dto.User
{
    public class UserDTO
    {
        public int Id { get; set; }  // Adicionada a propriedade Id
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
