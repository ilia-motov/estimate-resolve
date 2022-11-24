using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных объекта строительства.
    /// </summary>
    public class ConstructionObjectDto
    {
        /// <summary>
        /// Получает идентификатор объекта строительства.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Получает имя объекта строительства.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public ClientDto Client { get; set; }
    }
}
