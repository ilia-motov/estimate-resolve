namespace EstimateResolve.Entities
{
    /// <summary>
    /// Представляет класс приложения.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Прлучает или устанавливает идентификатор приложения.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или устанавливает идентификатор договора.
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Получает или устанавливает название приложения.
        /// </summary>
        public string Name { get; set; }


    }
}
