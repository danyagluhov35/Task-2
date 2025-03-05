namespace TestTask.Entity
{
    /// <summary>
    ///     Промежуточная таблица, для связи пользователя, и его заказов
    /// </summary>
    public class OrderUser
    {
        public string? UserId { get; set; } // Идентификатор пользователя.
        public User? User { get; set; } // Навигационное свойство для пользователя.

        public string? OrderId { get; set; } // Идентификатор заказа.
        public Order? Order { get; set; } // Навигационное свойство для заказа.
    }
}
