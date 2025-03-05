using Microsoft.EntityFrameworkCore;
using TestTask.Entity;
using TestTask.IService;

namespace TestTask.Service
{
    /// <summary>
    ///     Сервис для управления заказами.
    ///     <param name="db">Контекст базы данных.</param>
    ///     <param name="httpContextAccessor">Аксессор контекста HTTP, блягодаря ему можно регестрировать контект</param>
    ///     <param name="context">Сам контекст</param>
    ///     <param name="logger">Логгер.</param>
    /// </summary>
    public class OrderService : IOrderService
    {
        private ApplicationContext db;
        private HttpContext context;
        private IHttpContextAccessor httpContextAccessor;
        private ILogger<OrderService> Logger;
        public OrderService(ApplicationContext db, IHttpContextAccessor httpContextAccessor, ILogger<OrderService> logger)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            context = httpContextAccessor.HttpContext!;
            Logger = logger;
        }

        /// <summary>
        ///     Создает новый заказ и привязывает его к пользователю.
        ///     <param name="order">Объект заказа.</param>
        /// </summary>
        public async Task Create(Order order)
        {
            try
            {
                var userId = context.User.Claims.FirstOrDefault(u => u.Type == "Id")?.Value;
                db.Orders.Add(order);
                db.OrderUsers.Add(new OrderUser()
                {
                    UserId = userId,
                    OrderId = order.Id,
                });
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }

        /// <summary>
        ///     Удаляет заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Сообщение о результате операции.</returns>
        public async Task<string> Delete(string id)
        {
            try
            {
                var result = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if(result != null)
                {
                    db.Orders.Remove(result);
                    await db.SaveChangesAsync();
                    return "Заказ успешно удален";
                }
                return "Заказ не найден";
            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex.Message}");
                return "Ошибка при удалении";
            }
        }

        /// <summary>
        ///     Получает список заказов пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список заказов пользователя.</returns>
        public async Task<List<OrderUser>> GetAll(string userId)
        {
            try
            {
                return await db.OrderUsers
                    .Include(o => o.Order)
                    .Include(o => o.User)
                    .Where(u => u.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex.Message}");
                return new List<OrderUser>();
            }
        }
    }
}
