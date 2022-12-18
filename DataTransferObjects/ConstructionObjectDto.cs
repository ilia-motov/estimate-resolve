using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных объекта строительства.
    /// </summary>
    public class ConstructionObjectDto : IDto
    {
        /// <summary>
        /// Получает идентификатор объекта строительства.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает имя объекта строительства.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public ClientDto Client { get; set; }
    }
}
