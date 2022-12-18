using EstimateResolve.Entities;
using static MudBlazor.CategoryTypes;

namespace EstimateResolve.DataTransferObjects
{
    /// <summary>
    /// Представляет объект передачи данных, который содержит информацию о разбиении на страницы и элементы.
    /// </summary>
    public class ParginatedListDto<T> where T : IDto
    {
        /// <summary>
        /// Получает или задает общее количество элементов в списке.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// Получает или задает элементы текущей страницы.
        /// </summary>
        public List<T> Items { get; set; }
    }
}
