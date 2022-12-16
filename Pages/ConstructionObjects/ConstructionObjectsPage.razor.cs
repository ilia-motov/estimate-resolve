using EstimateResolve.Controllers;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Entities;
using EstimateResolve.Pages.Clients;
using EstimateResolve.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EstimateResolve.Pages.ConstructionObjects
{
    public partial class ConstructionObjectsPage
    {

        private MudTable<ConstructionObjectDto> _table;
        private string _searchString = string.Empty;
        private ConstructionObjectDto _itemBeforeEdit;

        [Inject]
        public IController<ClientDto> ClientController { get; set; }

        [Inject]
        public IController<ConstructionObjectDto> ConstructionObjectController { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server.
        /// </summary>
        private async Task<TableData<ConstructionObjectDto>> ServerReload(TableState state)
        {
            var data = await ConstructionObjectController.ReadAll(_searchString, state.SortLabel, state.SortDirection, state.Page + 1, state.PageSize);
            return new TableData<ConstructionObjectDto>() { TotalItems = (int)data.TotalItems, Items = data.Items };
        }

        private async Task OnSearch(string text)
        {
            _searchString = text;
            await _table.ReloadServerData();
        }

        private void BackupItem(object element)
        {
            var constructionObject = (ConstructionObjectDto)element;

            _itemBeforeEdit = new()
            {
                Id = constructionObject.Id,
                Name = constructionObject.Name,
                Client = new ClientDto()
                {
                    Id = constructionObject.Client.Id,
                    Name= constructionObject.Client.Name
                },
            };

            StateHasChanged();
        }

        private void ItemHasBeenCommitted(object element)
        {
            ConstructionObjectController.Update((ConstructionObjectDto)element);
            StateHasChanged();
        }

        private void ResetItemToOriginalValues(object element)
        {
            var constructionObject = (ConstructionObjectDto)element;

            constructionObject.Id = _itemBeforeEdit.Id;
            constructionObject.Name = _itemBeforeEdit.Name;
            constructionObject.Client = new ClientDto
            {
                Id = _itemBeforeEdit.Client.Id,
                Name = _itemBeforeEdit.Client.Name
            };

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
                await ClientController.Delete(item.Id);

            await _table.ReloadServerData();
        }

        private async Task Create()
        {
            var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            var dialogResult = await DialogService.Show<CreateConstructionObjectDialog>("Создание объекта", closeOnEscapeKey).Result;

            if (!dialogResult.Cancelled)
                await _table.ReloadServerData();
        }

        private async Task<IEnumerable<ClientDto>> SearchClient(string value) =>
            await ClientController.Autocomplete(value);
    }
}
