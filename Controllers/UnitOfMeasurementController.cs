﻿using System.Linq.Expressions;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Entities;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using TanvirArjel.EFCore.GenericRepository;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Предоставляет API для управления единицами измерения в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UnitOfMeasurementController : ControllerBase, IController<UnitOfMeasurementDto>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="UnitOfMeasurementController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UnitOfMeasurementController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("[action]")]
        public async Task<List<UnitOfMeasurementDto>> Autocomplete(string searchString)
        {
            var specification = new Specification<UnitOfMeasurement>
            {
                Conditions = new List<Expression<Func<UnitOfMeasurement, bool>>>
                {
                    x => string.IsNullOrWhiteSpace(searchString)
                    || x.Id.ToString().Contains(searchString.Trim())
                    || x.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                },
                Take = 10,
                OrderByDynamic = (nameof(UnitOfMeasurement.Name), SortDirection.Ascending.ToString())
            };

            return await _repository.GetListAsync(specification, x => new UnitOfMeasurementDto
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        /// <summary>
        /// Создает указанного <paramref name="unitOfMeasurement"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="unitOfMeasurement">единица измерения для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPut]
        public async Task<int> Create(UnitOfMeasurementDto unitOfMeasurementDto)
        {
            var unitOfMeasurement = new UnitOfMeasurement
            {
                Id = unitOfMeasurementDto.Id,
                Name = unitOfMeasurementDto.Name,
            };

            _repository.Add(unitOfMeasurement);
            await _repository.SaveChangesAsync();

            return unitOfMeasurement.Id;
        }

        /// <summary>
        /// Возвращает список заказчиков в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<UnitOfMeasurementDto>"/>
        /// операции.
        [HttpGet("[action]")]
        public async Task<ParginatedListDto<UnitOfMeasurementDto>> ReadAll(
            string searchString = "",
            string sortLabel = "",
            SortDirection sortDirection = SortDirection.Ascending,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var specification = new PaginationSpecification<UnitOfMeasurement>
            {
                Conditions = new List<Expression<Func<UnitOfMeasurement, bool>>>
                {
                    x => string.IsNullOrWhiteSpace(searchString)
                    || x.Id.ToString().Contains(searchString.Trim())
                    || x.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || (x.Id.ToString() + " " + x.Name).Contains(searchString.Trim().ToLower())
                },
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            if (sortLabel != null)
                specification.OrderByDynamic = (sortLabel, sortDirection == SortDirection.Ascending ? "Asc" : "Desc");

            if (sortLabel != null)
                specification.OrderByDynamic = (sortLabel, sortDirection.ToString());

            var paginatedList = await _repository.GetListAsync(specification, x => new UnitOfMeasurementDto
            {
                Id = x.Id,
                Name = x.Name,
            });

            return new ParginatedListDto<UnitOfMeasurementDto>
            {
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.Items,
            };
        }

        /// <summary>
        /// Находит и возвращает единицу измерения, если он есть, который имеет указанный <paramref name="unitOfMeasurementId"/>.
        /// </summary>
        /// <param name="unitOfMeasurementId">Идентификатор единицы измерения для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<UnitOfMeasurementDto> Read(int unitOfMeasurementId)
        {
            var unitOfMeasurement = await _repository.GetByIdAsync<Client>(unitOfMeasurementId);
            return new UnitOfMeasurementDto
            {
                Id = unitOfMeasurement.Id,
                Name = unitOfMeasurement.Name
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="unitOfMeasurementDto"/> в системе.
        /// </summary>
        /// <param name="unitOfMeasurementDto">единица измерения для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(UnitOfMeasurementDto unitOfMeasurementDto)
        {
            var unitOfMeasurement = await _repository.GetByIdAsync<UnitOfMeasurement>(unitOfMeasurementDto.Id);

            unitOfMeasurement.Id = unitOfMeasurementDto.Id;
            unitOfMeasurement.Name = unitOfMeasurementDto.Name;

            _repository.Update(unitOfMeasurement);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет единицы измерения по идентификатору из системы.
        /// </summary>
        /// <param name="id">Идентификатор единицы измерения для удаления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpDelete]
        public async Task Delete(int id)
        {
            var unitOfMeasurement = await _repository.GetByIdAsync<UnitOfMeasurement>(id);
            _repository.Remove(unitOfMeasurement);
            await _repository.SaveChangesAsync();
        }
    }
}
