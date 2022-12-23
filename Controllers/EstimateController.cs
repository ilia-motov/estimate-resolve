using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Предоставляет API для управления сметой в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EstimateController : ControllerBase, IController<EstimateDto>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="EstimateController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EstimateController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("[action]")]
        public async  Task<List<EstimateDto>> Autocomplete(string searchString)
        {
            var specification = new Specification<Estimate>
            {
                Conditions = new List<Expression<Func<Estimate, bool>>>
                {
                    x => string.IsNullOrWhiteSpace(searchString)
                    || x.Id.ToString().Contains(searchString.Trim())
                    || x.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                },
                Take = 10,
                OrderByDynamic = (nameof(Estimate.Name), SortDirection.Ascending.ToString())
            };

            return await _repository.GetListAsync(specification, x => new EstimateDto
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        /// <summary>
        /// Создает указанного <paramref name="estimateDto"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="estimateDto">смета для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPut]
        public async Task Create(EstimateDto estimateDto)
        {
            var estimate = new Estimate
            {
                Id = estimateDto.Id,
                Number = estimateDto.Number,
                Name = estimateDto.Name,
                DevelopmentDate = estimateDto.DevelopmentDate,
                ConstructionObjectId = estimateDto.ConstructionObject.Id,
                ClientId = estimateDto.Client.Id,
                ContractId = estimateDto.Contract.Id,
                EstimateWorks = estimateDto.Works
                    .Select(x => new EstimateWork
                    {
                        Id = x.Id,
                        CompanyServiceId = x.CompanyService.Id,
                        Value = x.Value,
                        Remark = x.Remark,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList(),
                EstimateMaterials = estimateDto.Materials
                    .Select(x => new EstimateMaterial
                    {
                        Id = x.Id,
                        EstimateWorkId = x.EstimateWorkId,
                        MaterialId = x.Material.Id,
                        Consumption = x.Consumption,
                        ValueWorking = x.ValueWorking,
                        Quantity= x.Quantity,
                        Price= x.Price,
                        Amount= x.Amount
                    })
                    .ToList(),
            };

            _repository.Add(estimate);

            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список услуг компании в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<EstimateDto>"/>
        /// операции.
        [HttpGet("[action]")]
        public async Task<ParginatedListDto<EstimateDto>> ReadAll(
            string searchString = "",
            string sortLabel = "",
            SortDirection sortDirection = SortDirection.Ascending,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var specification = new PaginationSpecification<Estimate>
            {
                Includes  = q => q
                    .Include(x => x.Contract)
                    .Include(x => x.Client)
                    .Include(x => x.ConstructionObject)
                    .Include(x => x.EstimateWorks)
                        .ThenInclude(x => x.Select(x => x.CompanyService))
                        .ThenInclude(x => x.Select(x => x.UnitOfMeasurement))
                    .Include(x => x.EstimateMaterials)
                        .ThenInclude(x => x.Select(x => x.Material)),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            if (sortLabel != null)
                specification.OrderByDynamic = (sortLabel, sortDirection == SortDirection.Ascending ? "Asc" : "Desc");

            var paginatedList = await _repository.GetListAsync(specification, x => new EstimateDto
            {
                Id = x.Id,
                Number = x.Number,
                Name = x.Name,
                DevelopmentDate = x.DevelopmentDate,
                ConstructionObject = new ConstructionObjectDto
                {
                    Id = x.ConstructionObject.Id,
                    Name = x.ConstructionObject.Name
                },
                Client = new ClientDto
                {
                    Id = x.Client.Id,
                    Name = x.Client.Name
                },
                Contract = new ContractDto
                {
                    Id = x.Contract.Id,
                    Name = x.Contract.Name
                },
                Works = x.EstimateWorks
                    .Select(x => new EstimateWorkDto
                    {
                        Id = x.Id,
                        CompanyService = new CompanyServiceDto
                        {
                            Id= x.CompanyService.Id,
                            Name = x.CompanyService.Name,
                            UnitOfMeasurement = new UnitOfMeasurementDto
                            {
                                Id= x.CompanyService.UnitOfMeasurement.Id,
                                Name = x.CompanyService.UnitOfMeasurement.Name,
                            }
                        },
                        Value = x.Value,
                        Remark = x.Remark,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList(),
                Materials = x.EstimateMaterials
                    .Select(x => new EstimateMaterialDto
                    {
                        Id = x.Id,
                        EstimateWorkId = x.EstimateWorkId,
                        Material = new MaterialDto
                        {
                            Id = x.Material.Id,
                            Name = x.Material.Name,
                        },
                        Consumption = x.Consumption,
                        ValueWorking = x.ValueWorking,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList()
            });

            return new ParginatedListDto<EstimateDto>
            {
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.Items,
            };
        }

        /// <summary>
        /// Находит и возвращает смету, если она есть, который имеет указанный <paramref name="estimateId"/>.
        /// </summary>
        /// <param name="estimateId">Идентификатор сметы для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<EstimateDto> Read(int estimateId)
        {
            var specification = new Specification<Estimate>
            {
                Conditions = new List<Expression<Func<Estimate, bool>>>
                {
                    x => x.Id == estimateId,
                },
                Includes = q => q
                    .Include(x => x.Contract)
                    .Include(x => x.Client)
                    .Include(x => x.ConstructionObject)
                    .Include(x => x.EstimateWorks)
                        .ThenInclude(x => x.Select(x => x.CompanyService))
                        .ThenInclude(x => x.Select(x => x.UnitOfMeasurement))
                    .Include(x => x.EstimateMaterials)
                        .ThenInclude(x => x.Select(x => x.Material))
            };

            var estimate = await _repository.GetAsync(specification);

            return new EstimateDto
            {
                Id = estimate.Id,
                Number = estimate.Number,
                Name = estimate.Name,
                DevelopmentDate = estimate.DevelopmentDate,
                ConstructionObject = new ConstructionObjectDto
                {
                    Id = estimate.ConstructionObject.Id,
                    Name = estimate.ConstructionObject.Name
                },
                Client = new ClientDto
                {
                    Id = estimate.Client.Id,
                    Name = estimate.Client.Name
                },
                Contract = new ContractDto
                {
                    Id = estimate.Contract.Id,
                    Name = estimate.Contract.Name
                },
                Works = estimate.EstimateWorks
                    .Select(x => new EstimateWorkDto
                    {
                        Id = x.Id,
                        CompanyService = new CompanyServiceDto
                        {
                            Id = x.CompanyService.Id,
                            Name = x.CompanyService.Name,
                            UnitOfMeasurement = new UnitOfMeasurementDto
                            {
                                Id = x.CompanyService.UnitOfMeasurement.Id,
                                Name = x.CompanyService.UnitOfMeasurement.Name,
                            }
                        },
                        Value = x.Value,
                        Remark = x.Remark,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList(),
                Materials = estimate.EstimateMaterials
                    .Select(x => new EstimateMaterialDto
                    {
                        Id = x.Id,
                        EstimateWorkId = x.EstimateWorkId,
                        Material = new MaterialDto
                        {
                            Id = x.Material.Id,
                            Name = x.Material.Name,
                        },
                        Consumption = x.Consumption,
                        ValueWorking = x.ValueWorking,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList()
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="estimateDto"/> в системе.
        /// </summary>
        /// <param name="estimateDto">смета для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(EstimateDto estimateDto)
        {
            var estimate = await _repository.GetByIdAsync<Estimate>(estimateDto.Id);

            estimate.Name = estimateDto.Name;
            estimate.Number = estimateDto.Number;
            estimate.DevelopmentDate = estimateDto.DevelopmentDate;
            estimate.ContractId = estimateDto.Contract.Id;
            estimate.ClientId = estimateDto.Client.Id;
            estimate.ConstructionObjectId = estimateDto.ConstructionObject.Id;
            estimate.EstimateWorks = (List<EstimateWork>)estimateDto.Works
                    .Select(x => new EstimateWork
                    {
                        Id = x.Id,
                        CompanyServiceId = x.CompanyService.Id,
                        Value = x.Value,
                        Remark = x.Remark,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList();
            estimate.EstimateMaterials = estimateDto.Materials
                    .Select(x => new EstimateMaterial
                    {
                        Id = x.Id,
                        EstimateWorkId = x.EstimateWorkId,
                        MaterialId = x.Material.Id,
                        Consumption = x.Consumption,
                        ValueWorking = x.ValueWorking,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Amount = x.Amount
                    })
                    .ToList();
            _repository.Update(estimate);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет смету по идентификатору из системы.
        /// </summary>
        /// <param name="id">смета для удаления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpDelete]
        public async Task Delete(int id)
        {
            var estimate = await _repository.GetByIdAsync<Estimate>(id);
            _repository.Remove(estimate);
            await _repository.SaveChangesAsync();
        }
    }
}
