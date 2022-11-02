namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс строки работы табличной части сметы.
    /// </summary>
    public class EstimateWork
    {
        /// <summary>
        /// Получает или устанавливает идентификатор работы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор сметы.
        /// </summary>
        public int EstimateId { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор услуги компании.
        /// </summary>
        public int CompanyServicesId { get; set; }

        /// <summary>
        /// Получает или устанавливает объем работ.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Получает или устанавливает примечание (различия по одинаковым видам работ).
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Получает или устанавливает расценку за работу.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или устанавливает стоимость работы.
        /// </summary>
        public decimal Amount { get; set; }

    }
}
