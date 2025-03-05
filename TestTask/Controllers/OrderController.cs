using Microsoft.AspNetCore.Mvc;
using TestTask.Entity;
using TestTask.IService;

namespace TestTask.Controllers
{
    /// <summary>
    ///     Контроллер для управления заказами.
    /// </summary>
    public class OrderController : Controller
    {
        /// <summary>
        ///     Сервис для работы с заказами.
        /// </summary>
        private IOrderService OrderService;
        public OrderController(IOrderService orderService) => OrderService = orderService;
        /// <summary>
        /// Возвращает представление страницы заказа.
        /// </summary>
        public IActionResult Order()
        {
            return View();
        }
        /// <summary>
        ///     Добавляет новый заказ и перенаправляет на список заказов.
        /// </summary>
        /// <param name="order">Объект заказа, полученный из формы.</param>
        /// <returns>Перенаправление на страницу со списком заказов.</returns>
        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            await OrderService.Create(order);
            return RedirectToAction("OrderList", "OrderList");
        }
    }
}
