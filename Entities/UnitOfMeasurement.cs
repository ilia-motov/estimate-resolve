namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс единицы измерения.
    /// </summary>
    public class UnitOfMeasurement
    {
        /// <summary>
        /// Прлучает или задает идентификатор единиц измерения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название единиц измерения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает список услуг компании.
        /// </summary>
        public List<CompanyService> CompanyServices { get; set; }
    }
}
