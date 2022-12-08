using EstimateResolve.Controllers;
using EstimateResolve.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EstimateResolve.Pages
{
    public partial class Clients
    {
        private MudTable<ClientDto> _table;
        private string _searchString = string.Empty;
        private ClientDto? _selectedItem;
        private ClientDto _itemBeforeEdit;
        private HashSet<ClientDto> _selectedItems = new HashSet<ClientDto>();
        private ClientDto newClient;

        [Inject]
        public IController<ClientDto> Controller { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server.
        /// </summary>
        private async Task<TableData<ClientDto>> ServerReload(TableState state)
        {
            var data = await Controller.ReadAll(_searchString, state.SortLabel, state.SortDirection, state.Page + 1, state.PageSize);
            return new TableData<ClientDto>() { TotalItems = (int)data.Item1, Items = data.Item2 };
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
            foreach (var item in _selectedItems)
                await Controller.Delete(item.Id);

            await _table.ReloadServerData();
        }

        private async Task Create(ClientDto newClient)
        {

            await Controller.Create(newClient);
        }
    }
}
