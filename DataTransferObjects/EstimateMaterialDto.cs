using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных материалов в работу.
    /// </summary>
    public class EstimateMaterialDto
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
        /// Получает или устанавливает идентификатор работы.
        /// </summary>
        public int EstimateWorkId { get; set; }

        /// <summary>
        /// Получает или устанавливает материал.
        /// </summary>
        public MaterialDto Material { get; set; }

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
