namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс услуги компании.
    /// </summary>
    public class CompanyService
    {
        /// <summary>
        /// Получает или устанавливает идентификатор услуг компании.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает наименования услуг компании.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор единицы измерения.
        /// </summary>
        public int UnitOfMeasurementId { get; set; }

        /// <summary>
        /// Получает или устанавливает единицу измерения.
        /// </summary>
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        /// <summary>
        /// Получает или устанавливает цену за услугу.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или устанавливает список работ.
        /// </summary>
        public List<EstimateWork> EstimateWorks { get; set; }

    }
}
