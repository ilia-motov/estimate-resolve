namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс сметы.
    /// </summary>
    public class Estimate
    {
        /// <summary>
        /// Получает или задает идентификатор сметы.
        /// </summary>
        public int Id { get; set; }

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
        /// Получает или задает идентификатор объекта строительства.
        /// </summary>
        public int ConstructionObjectId { get; set; }

        /// <summary>
        /// Получает или задает объект строительства.
        /// </summary>
        public ConstructionObject ConstructionObject { get; set; }

        /// <summary>
        /// Получает или задает идентификатор заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Получает или задает идентификатор договора.
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Получает или задает договор.
        /// </summary>
        public Contract Contract { get; set; }

        /// <summary>
        /// Получает или задает список работ.
        /// </summary>
        public List<EstimateWork> EstimateWorks { get; set; }

        /// <summary>
        /// Получает или задает список материалов в работу.
        /// </summary>
        public List<EstimateMaterial> EstimateMaterials { get; set; }
    }
}
