
using System.Collections.Generic;
using MudBlazor;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Представляет абстракцию контроллера.
    /// </summary>
    public interface IController<T>
    {
        Task Create(T entity);

        Task<(long, List<T>)> ReadAll(
            string searchString,
            string sortLabel,
            SortDirection sortDirection,
            int pageIndex = 1,
            int pageSize = 10);

        Task<T> Read(int id);

        Task Update(T dto);

        Task Delete(int id);
    }
}
