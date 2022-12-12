using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных материала.
    /// </summary>
    public class MaterialDto
    {
        /// <summary>
        /// Получает идентификатор материала.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает имя материала.
        /// </summary>S
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает единицу измерения материал.
        /// </summary>
        public string UnitsRev { get; set; }

        /// <summary>
        /// Получает или устанавливает цену за материал.
        /// </summary>
        public decimal Price { get; set; }
    }
}
