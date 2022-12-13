namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс услуги компании.
    /// </summary>
    public class CompanyService
    {
        /// <summary>
        /// Получает или задает идентификатор услуг компании.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает наименования услуг компании.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает идентификатор единицы измерения.
        /// </summary>
        public int UnitOfMeasurementId { get; set; }

        /// <summary>
        /// Получает или задает единицу измерения.
        /// </summary>
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        /// <summary>
        /// Получает или задает цену за услугу.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или задает список работ.
        /// </summary>
        public List<EstimateWork> EstimateWorks { get; set; }

    }
}
