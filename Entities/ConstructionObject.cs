namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс объекта строительства.
    /// </summary>
    public class ConstructionObject
    {
        /// <summary>
        /// Получает или устанавливает объекта строительства.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или устанавливает заказчика.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Получает или устанавливает название объекта строительства.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает список смет.
        /// </summary>
        public List<Estimate> Estimates { get; set; }

        /// <summary>
        /// Получает или устанавливает список договоров.
        /// </summary>
        public List<Contract> Contracts { get; set; }
    }
}
