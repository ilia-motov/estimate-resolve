using EstimateResolve.DataTransferObjects;
using EstimateResolve.Entities;
using Microsoft.AspNetCore.Mvc;
using TanvirArjel.EFCore.GenericRepository;

namespace EstimateResolve.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IRepository _repository;

        public ClientController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPut]
        public async Task Create(ClientDto client)
        {
            var entity = new Client
            {
                Id = client.Id,
                Name = client.Name,
            };

            _repository.Add(entity);
            await _repository.SaveChangesAsync();
        }

        [HttpGet("govno")]
        public async Task<List<ClientDto>> ReadAll(int pageIndex = 1, int pageSize = 10)
        {
            var specification = new PaginationSpecification<Client>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var paginatedList = await _repository.GetListAsync(specification, e => new ClientDto
            {
                Id = e.Id,
                Name = e.Name,
            });

            return paginatedList.Items;
        }

        [HttpGet("hui")]
        public async Task<ClientDto> Read(int id)
        {
            var client = await _repository.GetByIdAsync<Client>(id);
            return new ClientDto
            {
                Id = id,
                Name = client.Name
            };
        }

        [HttpPatch]
        public async Task Update(ClientDto clientDto)
        {
            var client = await _repository.GetByIdAsync<Client>(clientDto.Id);

            client.Id = clientDto.Id;
            client.Name = clientDto.Name;

            _repository.Update(client);
            await _repository.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            var client = await _repository.GetByIdAsync<Client>(id);
            _repository.Remove(client);
            await _repository.SaveChangesAsync();
        }
    }
}
