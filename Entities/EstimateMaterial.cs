namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс строки материала табличной части сметы.
    /// </summary>
    public class EstimateMaterial
    {
        /// <summary>
        /// Получает или задает идентификатор материалов для выполнения работ.
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
        /// Получает или задает идентификатор работы.
        /// </summary>
        public int EstimateWorkId { get; set; }

        /// <summary>
        /// Получает или задает работу.
        /// </summary>
        public EstimateWork EstimateWork { get; set; }

        /// <summary>
        /// Получает или задает идентификатор материала.
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Получает или задает материал.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// Получает или задает расход материала на единицу объема.
        /// </summary>
        public float Consumption { get; set; }


        /// <summary>
        /// Получает или задает объем работ.
        /// </summary>
        public float ValueWorking { get; set; }


        /// <summary>
        /// Получает или задает общий объем материала для работы.
        /// </summary>
        public float Quantity { get; set; }

        /// <summary>
        /// Получает или задает цену за материал.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или задает стоимость материала.
        /// </summary>
        public decimal Amount { get; set; }

    }
}
