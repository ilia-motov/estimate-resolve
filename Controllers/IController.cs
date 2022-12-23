
using EstimateResolve.DataTransferObjects;
using MudBlazor;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Представляет абстракцию контроллера.
    /// </summary>
    public interface IController<T> where T : IDto
    {
        Task<List<T>> Autocomplete(string searchString);

        Task Create(T dto);

        Task<ParginatedListDto<T>> ReadAll(
            string searchString = "",
            string sortLabel = "",
            SortDirection sortDirection = SortDirection.Ascending,
            int pageIndex = 1,
            int pageSize = 10);

        Task<T> Read(int id);

        Task Update(T dto);

        Task Delete(int id);
    }
}
