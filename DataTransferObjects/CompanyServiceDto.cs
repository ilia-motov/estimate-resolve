using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи данных услуги компании.
    /// </summary>
    public class CompanyServiceDto : IDto
    {
        /// <summary>
        /// Получает идентификатор услуги компании.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает имя услуги компании.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает услугу компании.
        /// </summary>
        public UnitOfMeasurementDto UnitOfMeasurement { get; set; }

        /// <summary>
        /// Получает или задает цену за услугу.
        /// </summary>
        public decimal Price { get; set; }
    }
}
