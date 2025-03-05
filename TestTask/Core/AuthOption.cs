using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestTask.Core
{
    /// <summary>
    ///     Класс, содержащий параметры конфигурации для генерации и валидации JWT токенов.
    /// </summary>
    public class AuthOption
    {
        // Секретный ключ для подписи и верификации JWT токенов. 
        // В реальных приложениях лучше использовать конфиг файлы для хранения ключа, например в appsettings.json, т.к хранения ключа в коде небезопасно
        private const string Key = "1b>Z6kh~+O9[L3ic>>GCNxZ[X>B.71,])xJV2!rM?=8dwJj[W3(K~I,8=*8Cg/i5~TF+C\\L+b9r?_8Ey/o2#Zk$?Fa)M:<+JY~!w";

        // Издатель токена (Issuer) - уникальный идентификатор сервиса или приложения, которое генерирует токены.
        public static string Issuer = "TestTask";

        // Для какого приложения или пользователей предназначен токен.
        public static string Audience = "UserTestTask";

        /// <summary>
        ///     Получить симметричный ключ для подписи JWT токенов.
        /// </summary>
        /// <returns>Симметричный ключ для подписи JWT токенов.</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
