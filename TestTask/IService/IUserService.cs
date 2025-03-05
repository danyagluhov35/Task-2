namespace TestTask.IService
{
    /// <summary>
    ///     Интерфейс для управления пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Создает нового пользователя
        /// </summary>
        Task Create();
    }
}
