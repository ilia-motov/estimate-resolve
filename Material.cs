namespace EstimateResolve
{
    /// <summary>
    /// Представляет класс материала
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Получает или устанавливает идентификатор материала.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает название материала.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает единицы измерения.
        /// </summary>
        public string UnitsRev { get; set; }

        /// <summary>
        /// Получает или устанавливает цену за материал.
        /// </summary>
        public decimal Price { get; set; }

    }
}
