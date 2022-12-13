namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс материала
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Получает или задает идентификатор материала.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название материала.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает единицы измерения.
        /// </summary>
        public string UnitsRev { get; set; }

        /// <summary>
        /// Получает или задает цену за материал.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или задает список материалов в работу.
        /// </summary>
        public List<EstimateMaterial> EstimateMaterials { get; set; }
    }
}
