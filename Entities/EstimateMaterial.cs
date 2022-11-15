namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс строки материала табличной части сметы.
    /// </summary>
    public class EstimateMaterial
    {
        /// <summary>
        /// Получает или устанавливает идентификатор материалов для выполнения работ.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор сметы.
        /// </summary>
        public int EstimateId { get; set; }

        /// <summary>
        /// Получает или устанавливает смету.
        /// </summary>
        public Estimate Estimate { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор работы.
        /// </summary>
        public int EstimateWorkId { get; set; }

        /// <summary>
        /// Получает или устанавливает работу.
        /// </summary>
        public EstimateWork EstimateWork { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор материала.
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// Получает или устанавливает материал.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// Получает или устанавливает расход материала на единицу объема.
        /// </summary>
        public float Consumption { get; set; }


        /// <summary>
        /// Получает или устанавливает объем работ.
        /// </summary>
        public float ValueWorking { get; set; }


        /// <summary>
        /// Получает или устанавливает общий объем материала для работы.
        /// </summary>
        public float Quantity { get; set; }

        /// <summary>
        /// Получает или устанавливает цену за материал.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или устанавливает стоимость материала.
        /// </summary>
        public decimal Amount { get; set; }

    }
}
