using EstimateResolve.Controllers;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Pages.Clients;
using EstimateResolve.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EstimateResolve.Pages.Materials
{
    public partial class MaterialsPage
    {
        private MudTable<MaterialDto> _table;
        private string _searchString = string.Empty;
        private MaterialDto _itemBeforeEdit;

        [Inject]
        public IController<MaterialDto> Controller { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server.
        /// </summary>
        private async Task<TableData<MaterialDto>> ServerReload(TableState state)
        {
            var data = await Controller.ReadAll(_searchString, state.SortLabel, state.SortDirection, state.Page + 1, state.PageSize);
            return new TableData<MaterialDto>() { TotalItems = (int)data.TotalItems, Items = data.Items };
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
                Id = ((MaterialDto)element).Id,
                Name = ((MaterialDto)element).Name,
                UnitsRev = ((MaterialDto)element).UnitsRev,
                Price = ((MaterialDto)element).Price
            };

            StateHasChanged();
        }

        private void ItemHasBeenCommitted(object element)
        {
            Controller.Update((MaterialDto)element);
            StateHasChanged();
        }

        private void ResetItemToOriginalValues(object element)
        {
            ((MaterialDto)element).Id = _itemBeforeEdit.Id;
            ((MaterialDto)element).Name = _itemBeforeEdit.Name;
            ((MaterialDto)element).UnitsRev = _itemBeforeEdit.UnitsRev;
            ((MaterialDto)element).Price = _itemBeforeEdit.Price;

            StateHasChanged();
        }

        private async Task Delete()
        {
            if (_table.SelectedItems.Count == 0)
                return;

            var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            var dialogResult = await DialogService.Show<DeleteDialog>("Удалить", closeOnEscapeKey).Result;

            if (dialogResult.Cancelled)
                return;

            foreach (var item in _table.SelectedItems)
                await Controller.Delete(item.Id);

            await _table.ReloadServerData();
        }

        private async Task Create()
        {
            var closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            var dialogResult = await DialogService.Show<CreateMaterialDialog>("Создание нового материала", closeOnEscapeKey).Result;

            if (!dialogResult.Cancelled)
                await _table.ReloadServerData();
        }
    }
}
