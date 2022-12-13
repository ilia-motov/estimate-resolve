
using System.Collections.Generic;
using EstimateResolve.DataTransferObjects;
using MudBlazor;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Представляет абстракцию контроллера.
    /// </summary>
    public interface IController<T> where T : class
    {
        Task Create(T entity);

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
