using EstimateResolve.Controllers;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Pages.Clients.Components;
using EstimateResolve.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EstimateResolve.Pages.Clients
{
    public partial class ClientsPage
    {
        private MudTable<ClientDto> _table;
        private string _searchString = string.Empty;
        private ClientDto _itemBeforeEdit;

        [Inject]
        public IController<ClientDto> Controller { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server.
        /// </summary>
        private async Task<TableData<ClientDto>> ServerReload(TableState state)
        {
            var data = await Controller.ReadAll(_searchString, state.SortLabel, state.SortDirection, state.Page + 1, state.PageSize);
            return new TableData<ClientDto>() { TotalItems = (int)data.TotalItems, Items = data.Items };
        }

        private async Task OnSearch(string text)
        {
             _searchString = text;
            await _table.ReloadServerData();
        }

        private void BackupItem(object element)
        {
            _itemBeforeEdit = new()
            {
                Id = ((ClientDto)element).Id,
                Name = ((ClientDto)element).Name
            };

            StateHasChanged();
        }

        private void ItemHasBeenCommitted(object element)
        {
            Controller.Update((ClientDto)element);
            StateHasChanged();
        }

        private void ResetItemToOriginalValues(object element)
        {
            ((ClientDto)element).Id = _itemBeforeEdit.Id;
            ((ClientDto)element).Name = _itemBeforeEdit.Name;

            StateHasChanged();
        }

        private async Task Delete()
        {
            if (_table.SelectedItems.Count == 0)
                return;

            var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            var dialogResult = await DialogService.Show<DeleteDataDialog>("Удалить", closeOnEscapeKey).Result;

            if (dialogResult.Cancelled)
                return;

            foreach (var item in _table.SelectedItems)
                await Controller.Delete(item.Id);

            await _table.ReloadServerData();
        }

        private async Task Create()
        {
            var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            var dialogResult = await DialogService.Show<CreateClientDialog>("Создание клиента", closeOnEscapeKey).Result;

            if (!dialogResult.Cancelled)
                await _table.ReloadServerData();
        }
    }
}
