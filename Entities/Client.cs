namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс заказчика.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Получает или задает идентификатор заказчика.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает имя заказчика.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает список договоров.
        /// </summary>
        public List<Contract> Contracts { get; set; }

        /// <summary>
        /// Получает или задает список объектов.
        /// </summary>
        public List<ConstructionObject> ConstructionObjects { get; set; }

        /// <summary>
        /// Получает или задает список смет.
        /// </summary>
        public List<Estimate> Estimates { get; set; }
    }
}
