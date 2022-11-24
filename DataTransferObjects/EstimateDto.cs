using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных сметы.
    /// </summary>
    public class EstimateDto
    {
        /// <summary>
        /// Получает или устанавливает идентификатор сметы.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Получает или устанавливает имя номера сметы.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Получает или устанавливает название сметы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает дату составления сметы.
        /// </summary>
        public DateTime DevelopmentDate { get; set; }

        /// <summary>
        /// Получает или устанавливает объект строительства.
        /// </summary>
        public ConstructionObjectDto ConstructionObject { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public ClientDto Client { get; set; }

        /// <summary>
        /// Получает или устанавливает договор.
        /// </summary>
        public ContractDto Contract { get; set; }

        /// <summary>
        /// Получает или устанавливает список работ.
        /// </summary>
        public List<EstimateWorkDto> Works { get; set; }

        /// <summary>
        /// Получает или устанавливает список материалов в работу.
        /// </summary>
        public List<EstimateMaterialDto> Materials { get; set; }
    }
}
