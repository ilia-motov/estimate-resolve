using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных сметы.
    /// </summary>
    public class EstimateDto
    {
        /// <summary>
        /// Получает или задает идентификатор сметы.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Получает или задает имя номера сметы.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Получает или задает название сметы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает дату составления сметы.
        /// </summary>
        public DateTime DevelopmentDate { get; set; }

        /// <summary>
        /// Получает или задает объект строительства.
        /// </summary>
        public ConstructionObjectDto ConstructionObject { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public ClientDto Client { get; set; }

        /// <summary>
        /// Получает или задает договор.
        /// </summary>
        public ContractDto Contract { get; set; }

        /// <summary>
        /// Получает или задает список работ.
        /// </summary>
        public List<EstimateWorkDto> Works { get; set; }

        /// <summary>
        /// Получает или задает список материалов в работу.
        /// </summary>
        public List<EstimateMaterialDto> Materials { get; set; }
    }
}
