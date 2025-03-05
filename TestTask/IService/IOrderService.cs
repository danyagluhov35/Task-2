using TestTask.Entity;

namespace TestTask.IService
{
    /// <summary>
    ///     Интерфейс сервиса для управления заказами.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        ///     Создает новый заказ.
        /// </summary>
        Task Create(Order order);
        /// <summary>
        ///     Удаляет заказ по его идентификатору.
        /// </summary>
        Task<string> Delete(string id);
        /// <summary>
        ///     Получает список заказов пользователя по его идентификатору
        /// </summary>
        Task<List<OrderUser>> GetAll(string userId);
    }
}
