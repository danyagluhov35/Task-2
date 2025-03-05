namespace TestTask.Entity
{
    /// <summary>
    ///     Класс пользователя
    /// </summary>
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString().Substring(0,8);
            Name = "Гость";
        }
        public string Id { get; set; } // Идентификатор
        public string? Name { get; set; } // Имя пользователя, по умолчанию - Гость

        public List<OrderUser> OrderUsers { get; set; } = new(); // Связь один ко многим
    }
}
