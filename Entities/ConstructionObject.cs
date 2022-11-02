namespace EstimateResolve
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
        /// Получает или устанавливает идентификатор заказчика.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Получает или устанавливает название объекта строительства.
        /// </summary>
        public string Name { get; set; }
    }
}
