using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных заказчика.
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Получает идентификатор заказчика.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает имя заказчика.
        /// </summary>
        public string Name { get; set; }
    }
}
