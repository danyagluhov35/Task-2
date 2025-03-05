using Microsoft.AspNetCore.Mvc;
using TestTask.Entity;
using TestTask.IService;

namespace TestTask.Controllers
{
    /// <summary>
    ///     Контроллер для управления списком заказов.
    ///     
    ///     <param name="db">Экземпляр контекста базы данных.</param>
    ///     <param name="orderService">Экземпляр сервиса заказов.</param>
    /// </summary>
    public class OrderListController : Controller
    {
        private readonly ApplicationContext db;
        private IOrderService OrderService;
        public OrderListController(ApplicationContext db, IOrderService orderService)
        {
            this.db = db;
            OrderService = orderService;
        }

        /// <summary>
        ///     Получает список заказов текущего пользователя.
        /// </summary>
        /// <returns>Представление со списком заказов.</returns>
        public async Task<IActionResult> OrderList()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "Id")?.Value; // Получение идентификатора текущего пользователя из Claims
            var result = await OrderService.GetAll(userId!); // Загрузка всех заказов пользователя
            return View(result);
        }
        /// <summary>
        ///     Удаляет заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>JSON-ответ с сообщением об удалении.</returns>
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var result = await OrderService.Delete(id);

            return new JsonResult(new { message = result });
        }
    }
}
