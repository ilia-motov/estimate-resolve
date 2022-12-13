using EstimateResolve.Controllers;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Pages.Clients;
using EstimateResolve.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EstimateResolve.Pages.UnitOfMeasurements
{
    public partial class UnitOfMeasurementPage
    {
        private MudTable<UnitOfMeasurementDto> _table;
        private string _searchString = string.Empty;
        private UnitOfMeasurementDto _itemBeforeEdit;

        [Inject]
        public IController<UnitOfMeasurementDto> Controller { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server.
        /// </summary>
        private async Task<TableData<UnitOfMeasurementDto>> ServerReload(TableState state)
        {
            var data = await Controller.ReadAll(_searchString, state.SortLabel, state.SortDirection, state.Page + 1, state.PageSize);
            return new TableData<UnitOfMeasurementDto>() { TotalItems = (int)data.TotalItems, Items = data.Items };
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
                Id = ((UnitOfMeasurementDto)element).Id,
                Name = ((UnitOfMeasurementDto)element).Name
            };

            StateHasChanged();
        }

        private void ItemHasBeenCommitted(object element)
        {
            Controller.Update((UnitOfMeasurementDto)element);
            StateHasChanged();
        }

        private void ResetItemToOriginalValues(object element)
        {
            ((UnitOfMeasurementDto)element).Id = _itemBeforeEdit.Id;
            ((UnitOfMeasurementDto)element).Name = _itemBeforeEdit.Name;

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
            var dialogResult = await DialogService.Show<CreateUnitOfMeasurementDialog>("Создание единицы измерения", closeOnEscapeKey).Result;

            if (!dialogResult.Cancelled)
                await _table.ReloadServerData();
        }
    }
}
