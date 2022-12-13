using System.Linq.Expressions;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using TanvirArjel.EFCore.GenericRepository;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Предоставляет API для управления договором в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class СontractController : ControllerBase, IController<ContractDto>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="СontractController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public СontractController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Создает указанного <paramref name="contract"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="contract">договор для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPut]
        public async Task Create(ContractDto contractDto)
        {
            var contract = new Contract
            {
                Id = contractDto.Id,
                Name = contractDto.Name,
                ClientId = contractDto.Client.Id,
                ConstructionObjectId = contractDto.ConstructionObject.Id
            };

            _repository.Add(contract);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список договоров в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<ContractDto>"/>
        /// операции.
        [HttpGet("[action]")]
        public async Task<ParginatedListDto<ContractDto>> ReadAll(
            string searchString,
            string sortLabel,
            SortDirection sortDirection,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var specification = new PaginationSpecification<Contract>
            {
                Includes = x => x.Include(x => x.Client).Include(x => x.ConstructionObject),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var paginatedList = await _repository.GetListAsync(specification, x => new ContractDto
            {
                Id = x.Id,
                Name = x.Name,
                Client = new ClientDto
                {
                    Id = x.Client.Id,
                    Name = x.Client.Name
                },
                ConstructionObject = new ConstructionObjectDto
                {
                    Id = x.ConstructionObject.Id,
                    Name = x.ConstructionObject.Name
                }
            });

            return new ParginatedListDto<ContractDto>
            {
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.Items,
            };
        }

        /// <summary>
        /// Находит и возвращает договор, если он есть, который имеет указанный <paramref name="contractId"/>.
        /// </summary>
        /// <param name="contractId">Идентификатор договора для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ContractDto> Read(int contractId)
        {
            var specification = new Specification<Contract>
            {
                Conditions = new List<Expression<Func<Contract, bool>>>
                {
                    x => x.Id == contractId,
                },
                Includes = x => x.Include(x => x.Client).Include(x => x.ConstructionObject)
            };

            var contract = await _repository.GetByIdAsync<Contract>(contractId);

            return new ContractDto
            {
                Id = contract.Id,
                Name = contract.Name,
                Client = new ClientDto
                {
                    Id = contract.Client.Id,
                    Name = contract.Client.Name
                },
                ConstructionObject = new ConstructionObjectDto
                {
                    Id = contract.ConstructionObject.Id,
                    Name = contract.ConstructionObject.Name,
                }
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="contractDto"/> в системе.
        /// </summary>
        /// <param name="contractDto">договор для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(ContractDto contractDto)
        {
            var contract = await _repository.GetByIdAsync<ConstructionObject>(contractDto.Id);

            contract.Name = contractDto.Name;
            contract.ClientId = contractDto.Client.Id;

            _repository.Update(contract);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет договор по идентификатору из системы.
        /// </summary>
        /// <param name="id">объект строительства для удаления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpDelete]
        public async Task Delete(int id)
        {
            var contract = await _repository.GetByIdAsync<Contract>(id);
            _repository.Remove(contract);
            await _repository.SaveChangesAsync();
        }
    }
}
