using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных договора.
    /// </summary>
    public class ContractDto : IDto
    {
        /// <summary>
        /// Получает идентификатор договора.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает имя договора.
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public ClientDto Client { get; set;}

        /// <summary>
        /// Получает или задает объект строительства.
        /// </summary>
        public ConstructionObjectDto ConstructionObject { get; set;}
    }
}
