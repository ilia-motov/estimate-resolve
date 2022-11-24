using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных договора.
    /// </summary>
    public class ContractDto
    {
        /// <summary>
        /// Получает идентификатор договора.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Получает или устанавливает имя договора.
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public ClientDto Client { get; set;}

        /// <summary>
        /// Получает или устанавливает объект строительства.
        /// </summary>
        public ConstructionObjectDto ConstructionObject { get; set;}
    }
}
