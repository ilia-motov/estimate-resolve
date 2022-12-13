namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс объекта строительства.
    /// </summary>
    public class ConstructionObject
    {
        /// <summary>
        /// Получает или задает объекта строительства.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или задает заказчика.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Получает или задает название объекта строительства.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает список смет.
        /// </summary>
        public List<Estimate> Estimates { get; set; }

        /// <summary>
        /// Получает или задает список договоров.
        /// </summary>
        public List<Contract> Contracts { get; set; }
    }
}
