using EstimateResolve.Entities;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет класс объекта передачи работ сметы.
    /// </summary>
    public class EstimateWorkDto
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
        /// Получает или задает услугу компании.
        /// </summary>
        public CompanyServiceDto CompanyService { get; set; }

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
    }
}
