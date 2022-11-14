namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс единицы измерения.
    /// </summary>
    public class UnitOfMeasurement
    {
        /// <summary>
        /// Прлучает или устанавливает идентификатор единиц измерения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает название единиц измерения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или устанавливает список услуг компании.
        /// </summary>
        public List<CompanyService> CompanyServices { get; set; }
    }
}
