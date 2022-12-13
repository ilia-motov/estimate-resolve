namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс договора.
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// Получает или задает идентификатор договора.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает имя договора.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public int ConstructionObjectId { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public ConstructionObject ConstructionObject { get; set; }

        /// <summary>
        /// Получает или задает список смет.
        /// </summary>
        public List<Estimate> Estimates { get; set; }
    }
}
