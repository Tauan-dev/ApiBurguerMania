using ApiBurguerMania.Dto.User;
using ApiBurguerMania.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Injeção de dependência
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Obter todos os usuários
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Obter um usuário por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                // Caso o usuário não seja encontrado
                return NotFound("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Criar um novo usuário
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Os dados do usuário são obrigatórios.");
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (ArgumentNullException ex)
            {
                // Caso os dados sejam nulos ou inválidos
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Atualizar um usuário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Os dados do usuário são obrigatórios.");
            }

            try
            {
                var success = await _userService.UpdateUserAsync(id, userDto);
                if (success)
                {
                    return NoContent(); // Sucesso, sem conteúdo a retornar
                }

                // Caso o usuário não seja encontrado
                return NotFound("Usuário não encontrado");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Deletar um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var success = await _userService.DeleteUserAsync(id);
                if (success)
                {
                    return NoContent(); // Sucesso, sem conteúdo a retornar
                }

                // Caso o usuário não seja encontrado
                return NotFound("Usuário não encontrado");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuário não encontrado");
            }
            catch (Exception ex)
            {
        
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}
