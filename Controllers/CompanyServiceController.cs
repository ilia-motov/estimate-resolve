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
    /// Предоставляет API для управления услугой компании в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyServiceController : ControllerBase, IController<CompanyServiceDto>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="ConstructionObjectController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CompanyServiceController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("[action]")]
        public Task<List<CompanyServiceDto>> Autocomplete(string searchString) => throw new NotImplementedException();

        /// <summary>
        /// Создает указанного <paramref name="companyService"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="companyService">услуга компании для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPut]
        public async Task Create(CompanyServiceDto companyServiceDto)
        {
            var companyService = new CompanyService
            {
                Id = companyServiceDto.Id,
                Name = companyServiceDto.Name,
                UnitOfMeasurementId = companyServiceDto.UnitOfMeasurement.Id,
                Price = companyServiceDto.Price
            };

            _repository.Add(companyService);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список услуг компании в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<CompanyServiceDto>"/>
        /// операции.
        [HttpGet("[action]")]
        public async Task<ParginatedListDto<CompanyServiceDto>> ReadAll(
            string searchString = "",
            string sortLabel = "",
            SortDirection sortDirection = SortDirection.Ascending,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var specification = new PaginationSpecification<CompanyService>
            {
                Includes = x => x.Include(x => x.UnitOfMeasurement),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var paginatedList = await _repository.GetListAsync(specification, x => new CompanyServiceDto
            {
                Id = x.Id,
                Name = x.Name,
                UnitOfMeasurement = new UnitOfMeasurementDto
                {
                    Id= x.UnitOfMeasurement.Id,
                    Name = x.UnitOfMeasurement.Name
                },
                Price = x.Price
            });

            return new ParginatedListDto<CompanyServiceDto>
            {
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.Items,
            };
        }

        /// <summary>
        /// Находит и возвращает услугу компании, если она есть, который имеет указанный <paramref name="companyServiceId"/>.
        /// </summary>
        /// <param name="companyServiceId">Идентификатор услуги компании для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<CompanyServiceDto> Read(int companyServiceId)
        {
            var specification = new Specification<CompanyService>
            {
                Conditions = new List<Expression<Func<CompanyService, bool>>>
                {
                    x => x.Id == companyServiceId
                },
                Includes = x => x.Include(x => x.UnitOfMeasurement)
            };

            var companyService = await _repository.GetAsync(specification);

            return new CompanyServiceDto
            {
                Id = companyService.Id,
                Name = companyService.Name,
                UnitOfMeasurement = new UnitOfMeasurementDto
                {
                    Id = companyService.UnitOfMeasurement.Id,
                    Name = companyService.UnitOfMeasurement.Name
                },
                Price = companyService.Price
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="companyServiceDto"/> в системе.
        /// </summary>
        /// <param name="companyServiceDto">услуга компании для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(CompanyServiceDto companyServiceDto)
        {
            var companyService = await _repository.GetByIdAsync<CompanyService>(companyServiceDto.Id);

            companyService.Name = companyServiceDto.Name;
            companyService.UnitOfMeasurementId = companyServiceDto.UnitOfMeasurement.Id;

            _repository.Update(companyService);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет услугу компании по идентификатору из системы.
        /// </summary>
        /// <param name="id">услуга компании для удаления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpDelete]
        public async Task Delete(int id)
        {
            var companyService = await _repository.GetByIdAsync<CompanyService>(id);
            _repository.Remove(companyService);
            await _repository.SaveChangesAsync();
        }
    }
}
