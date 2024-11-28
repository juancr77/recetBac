using Microsoft.AspNetCore.Mvc;
using recetBac.Data;
using recetBac.Models;
using System.Linq;
using recetBac.Data;
using recetBac.Models;

namespace ComidaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Registro de usuario
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest("El nombre de usuario ya está registrado.");

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // Login por usuario y contraseña
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginRequest.Username);

            if (user == null)
                return Unauthorized("Usuario no encontrado.");

            if (user.Password != loginRequest.Password)
                return Unauthorized("Contraseña incorrecta.");

            return Ok(new { Message = "Login exitoso", UserId = user.UserId });
        }

        // Obtener todos los usuarios
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users.ToList());
        }

        // Obtener usuario por ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            return Ok(user);
        }

        // Actualizar usuario
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            user.FullName = updatedUser.FullName;
            user.FavoriteFood = updatedUser.FavoriteFood;
            user.City = updatedUser.City;
            user.Country = updatedUser.Country;

            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok(user);
        }

        // Eliminar usuario
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new { Message = "Usuario eliminado con éxito" });
        }
    }
}
