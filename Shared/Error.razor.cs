using EstimateResolve.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EstimateResolve.Shared
{
    public partial class Error : IErrorComponent
    {
        [Inject]
        public IExceptionInterceptorService ExceptionInterceptorService { get; set; }

        [Inject]
        public ILogger<Error> Logger { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void OnInitialized() => ExceptionInterceptorService.ExceptionComponent = this;

        public void ProcessError(Exception exception)
        {
            Snackbar.Add(exception.Message, Severity.Warning);
            Logger.LogError("Error:ProcessError - Type: {Type} Message: {Message}",
                exception.GetType(),
                exception.Message);
        }
    }
}
