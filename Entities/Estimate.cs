namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс сметы.
    /// </summary>
    public class Estimate
    {
        /// <summary>
        /// Получает или устанавливает идентификатор сметы.
        /// </summary>
        public int Id { get; set; }

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
        /// Получает или устанавливает идентификатор объекта строительства.
        /// </summary>
        public int ConstructionObjectId { get; set; }

        /// <summary>
        /// Получает или устанавливает объект строительства.
        /// </summary>
        public ConstructionObject ConstructionObject { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор договора.
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Получает или устанавливает договор.
        /// </summary>
        public Contract Contract { get; set; }

        /// <summary>
        /// Получает или устанавливает список работ.
        /// </summary>
        public List<EstimateWork> EstimateWorks { get; set; }

        /// <summary>
        /// Получает или устанавливает список материалов в работу.
        /// </summary>
        public List<EstimateMaterial> EstimateMaterials { get; set; }
    }
}
