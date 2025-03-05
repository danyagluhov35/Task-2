using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TestTask.Core;
using TestTask.Entity;
using TestTask.IService;

namespace TestTask.Middleware
{
    /// <summary>
    ///     Middleware для обработки JWT-токенов и аутентификации пользователей.
    ///     <param name="next">Следующий middleware в конвейере.</param>
    ///     <param name="serviceProvider">Провайдер зависимостей для создания Scope сервиса</param>
    /// </summary>
    public class JwtSecurity
    {
        private readonly RequestDelegate next;
        private readonly IServiceProvider ServiceProvider;

        public JwtSecurity(RequestDelegate next, IServiceProvider serviceProvider)
        {
            this.next = next;
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        ///     Метод для обработки HTTP-запроса, проверяет JWT-токен, аутентифицирует пользователя.
        /// </summary>
        /// <param name="context">HTTP-контекст запроса.</param>
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["JwtToken"];

            using (var scope = ServiceProvider.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        // Проверяется валидность токена и устанавливается в контекст
                        ClaimsPrincipal claimsPrincipal = await ValidateToken(token);
                        context.User = claimsPrincipal;
                    }
                    catch (SecurityTokenExpiredException)
                    {
                        // Удаляется истекший токен
                        context.Response.Cookies.Delete("JwtToken");
                    }
                }
                else
                {
                    await userService.Create();
                }
            }

            // Передаем управление следующему middleware
            await next.Invoke(context);
        }

        /// <summary>
        ///     Проверяет и валидирует JWT-токен.
        /// </summary>
        /// <param name="token">JWT-токен пользователя.</param>
        /// <returns>Объект ClaimsPrincipal с данными из токена.</returns>
        public async Task<ClaimsPrincipal> ValidateToken(string token)
        {
            return new ClaimsPrincipal(new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AuthOption.Issuer,
                ValidAudience = AuthOption.Audience,
                IssuerSigningKey = AuthOption.GetSymmetricSecurityKey()
            }, out SecurityToken securityToken));
        }
    }
}
