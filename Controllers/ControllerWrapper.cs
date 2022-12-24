using EstimateResolve.DataTransferObjects;
using EstimateResolve.Services;
using EstimateResolve.Shared;
using MudBlazor;

namespace EstimateResolve.Controllers
{
    public class ControllerWrapper<T> : IController<T> where T : class, IDto
    {
        private readonly IController<T> _controller;
        private readonly IExceptionInterceptorService _exceptionInterceptorService;

        public ControllerWrapper(IController<T> controller, IExceptionInterceptorService exceptionInterceptorService)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _exceptionInterceptorService = exceptionInterceptorService ?? throw new ArgumentNullException(nameof(exceptionInterceptorService));
        }

        public Task<List<T>> Autocomplete(string searchString) => TryExecute(() => _controller.Autocomplete(searchString));

        public Task<int> Create(T entity) => TryExecute(() => _controller.Create(entity));

        public Task<ParginatedListDto<T>> ReadAll(
            string searchString = "",
            string sortLabel = "",
            SortDirection sortDirection = SortDirection.Ascending,
            int pageIndex = 1,
            int pageSize = 10) => TryExecute(() => _controller.ReadAll(searchString, sortLabel, sortDirection, pageIndex, pageSize));

        public Task<T> Read(int id) => TryExecute(() => _controller.Read(id));

        public Task Update(T dto) => TryExecute(() => _controller.Update(dto));

        public Task Delete(int id) => TryExecute(() => _controller.Delete(id));

        private async Task TryExecute(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception exception)
            {
                _exceptionInterceptorService.ProcessException(exception);
            }
        }

        private async Task<TResult> TryExecute<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception exception)
            {

                _exceptionInterceptorService.ProcessException(exception);
                return default;
            }
        }
    }
}
