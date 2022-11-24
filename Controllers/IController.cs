using EstimateResolve.DataTransferObjects;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Представляет абстракцию контроллера.
    /// </summary>
    public interface IController<T>
    {
        Task Create(T client);

        Task<List<T>> ReadAll(int pageIndex = 1, int pageSize = 10);

        Task<T> Read(int id);

        Task Update(T dto);

        Task Delete(int id);
    }
}
