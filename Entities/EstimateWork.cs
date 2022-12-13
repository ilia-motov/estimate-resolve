namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс строки работы табличной части сметы.
    /// </summary>
    public class EstimateWork
    {
        /// <summary>
        /// Получает или задает идентификатор работы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает идентификатор сметы.
        /// </summary>
        public int EstimateId { get; set; }

        /// <summary>
        /// Получает или задает смету.
        /// </summary>
        public Estimate Estimate { get; set; }

        /// <summary>
        /// Получает или задает идентификатор услуги компании.
        /// </summary>
        public int CompanyServiceId { get; set; }

        /// <summary>
        /// Получает или задает услугу компании.
        /// </summary>
        public CompanyService CompanyService { get; set; }

        /// <summary>
        /// Получает или задает объем работ.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Получает или задает примечание (различия по одинаковым видам работ).
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Получает или задает расценку за работу.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или задает стоимость работы.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Получает или задает список материалов в работу.
        /// </summary>
        public List<EstimateMaterial> EstimateMaterials { get; set; }
    }
}
