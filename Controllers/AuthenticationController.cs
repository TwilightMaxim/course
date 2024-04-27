using Microsoft.AspNetCore.Mvc;
using PassengerTransportationAPI.Models;

namespace PassengerTransportationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationContext _context;
        public AuthenticationController(ApplicationContext CityMove)
        {
            _context = CityMove;
        }
        /// <summary>
        /// Метод авторизации пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Authorization")]
        public IActionResult Auth(string email, string password)
        {
            UserDatas? user = _context.UserData.SingleOrDefault(p => p.Email == email);
            if (user != null)
            {
                if (Verify(password, user.PasswordSalt, user.PasswordHash) == true)
                {
                    return Ok();
                }
                return StatusCode(400);
            }
            return BadRequest("Неправильный логин или пароль");
        }
        /// <summary>
        /// Метод регистрации пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationDoctor(UserDatas user)
        {
            (string hashedPassword, string salt) = Generate(user.PasswordHash);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;
            _context.UserData.Add(user);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        /// <summary>
        /// Проверка пароля на правильность
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool Verify(string password, string salt, string passwordHash)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword.Trim() == passwordHash.Trim());
        }
        /// <summary>
        /// Метод генерации зашифровонного пароля пользователя
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public (string, string) Generate(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword, salt);
        }
    }
}
