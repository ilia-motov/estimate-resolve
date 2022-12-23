using EstimateResolve.Services;

namespace EstimateResolve.Services
{
    public interface IExceptionInterceptorService
    {
        IErrorComponent? ExceptionComponent { get; set; }

        void ProcessException(Exception exception);
    }

    public class ExceptionInterceptorService : IExceptionInterceptorService
    {
        public IErrorComponent? ExceptionComponent { get; set; }

        public void ProcessException(Exception exception) => ExceptionComponent?.ProcessError(exception);
    }

    public interface IErrorComponent
    {
        void ProcessError(Exception exception);
    }
}
