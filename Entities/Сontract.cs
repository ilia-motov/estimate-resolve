namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс договора.
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// Получает или устанавливает идентификатор договора.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает имя договора.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public int ConstructionObjectId { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public ConstructionObject ConstructionObject { get; set; }

        /// <summary>
        /// Получает или устанавливает список смет.
        /// </summary>
        public List<Estimate> Estimates { get; set; }
    }
}
