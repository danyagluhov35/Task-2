using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestTask.Core;
using TestTask.Entity;
using TestTask.IService;

namespace TestTask.Service
{
    /// <summary>
    ///     Сервис для управления пользователями
    ///     Позволяет создавать нового пользователя, а именно гостя
    ///     
    ///     <param name="db">Контекст базы данных.</param>
    ///     <param name="httpContextAccessor">Аксессор контекста HTTP.</param>
    ///     <param name="context;">Сам контекст</param>
    /// </summary>
    public class UserService : IUserService
    {
        private ApplicationContext db;
        private HttpContext context;
        private IHttpContextAccessor httpContextAccessor;
        private ILogger<UserService> Logger;
        public UserService(ApplicationContext db, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            context = httpContextAccessor.HttpContext!;
            Logger = logger;
        }
        /// <summary>
        ///     Создает нового пользователя (гостя), добавляет его в базу данных
        ///     и устанавливает JWT-токен в cookies.
        /// </summary>
        public async Task Create()
        {
            try
            {
                User user = new User();

                // Создание списка Claim для пользователя
                var claim = new List<Claim>()
                {
                    new Claim("Id", user.Id!),
                    new Claim(ClaimTypes.Name, user.Name!)
                };

                // Создание нового токена
                var jwt = new JwtSecurityToken
                (
                    issuer: AuthOption.Issuer,
                    audience: AuthOption.Audience,
                    claims: claim,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

                // Добавление токена в куки
                context.Response.Cookies.Append("JwtToken", new JwtSecurityTokenHandler().WriteToken(jwt));

                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
