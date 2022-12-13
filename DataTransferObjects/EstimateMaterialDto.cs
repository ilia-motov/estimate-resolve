using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных материалов в работу.
    /// </summary>
    public class EstimateMaterialDto
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
        /// Получает или задает идентификатор работы.
        /// </summary>
        public int EstimateWorkId { get; set; }

        /// <summary>
        /// Получает или задает материал.
        /// </summary>
        public MaterialDto Material { get; set; }

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
