using System.ComponentModel.DataAnnotations;

namespace TestTask.Entity
{
    /// <summary>
    ///     Класс заказов
    /// </summary>
    public class Order
    {
        public Order() 
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; } // Уникальный идентификатор заказа 
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string? SenderCity { get; set; } // Город отправителя
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string? SenderAddress { get; set; } // Адрес отправителя
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string? ReceiverCity { get; set; } // Город получателя
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string? ReceiverAddress { get; set; } // Адрес получателя
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [Range(0, double.MaxValue, ErrorMessage = "Вес не может быть отрицательным")]
        public double? Weight { get; set; } // Вес груза
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public DateTime? CollectionDate { get; set; } // Время забора груза

        public List<OrderUser> OrderUsers { get; set; } = new(); // Навигационное свойство для связи заказов и пользователей
    }
}
