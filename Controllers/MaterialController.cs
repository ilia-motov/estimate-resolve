﻿using System.Linq.Expressions;
using EstimateResolve.DataTransferObjects;
using EstimateResolve.Entities;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using TanvirArjel.EFCore.GenericRepository;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Предоставляет API для управления материалом в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase, IController<MaterialDto>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="MaterialController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MaterialController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("[action]")]
        public async Task<List<MaterialDto>> Autocomplete(string searchString)
        {
            var specification = new Specification<Material>
            {
                Conditions = new List<Expression<Func<Material, bool>>>
                {
                    x => string.IsNullOrWhiteSpace(searchString)
                    || x.Id.ToString().Contains(searchString.Trim())
                    || x.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                },
                Take = 10,
                OrderByDynamic = (nameof(Material.Name), SortDirection.Ascending.ToString())
            };

            return await _repository.GetListAsync(specification, x => new MaterialDto
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        /// <summary>
        /// Создает указанного <paramref name="material"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="material">материал для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPut]
        public async Task<int> Create(MaterialDto materialDto)
        {
            var material = new Material
            {
                Id = materialDto.Id,
                Name = materialDto.Name,
                UnitsRev = materialDto.UnitsRev,
                Price = materialDto.Price,
            };

            _repository.Add(material);
            await _repository.SaveChangesAsync();

            return material.Id;
        }

        /// <summary>
        /// Возвращает список материалов в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<MaterialDto>"/>
        /// операции.
        [HttpGet("[action]")]
        public async Task<ParginatedListDto<MaterialDto>> ReadAll(
            string searchString = "",
            string sortLabel = "",
            SortDirection sortDirection = SortDirection.Ascending,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var specification = new PaginationSpecification<Material>
            {
                Conditions = new List<Expression<Func<Material, bool>>>
                {
                    x => string.IsNullOrWhiteSpace(searchString)
                    || x.Id.ToString().Contains(searchString.Trim())
                    || x.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || x.UnitsRev.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || x.Price.ToString().Contains(searchString.Trim().ToLower())
                    || (x.Id.ToString() + " " + x.Name + x.UnitsRev + x.Price).Contains(searchString.Trim().ToLower())
                },
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            if (sortLabel != null)
                specification.OrderByDynamic = (sortLabel, sortDirection == SortDirection.Ascending ? "Asc" : "Desc");

            var paginatedList = await _repository.GetListAsync(specification, x => new MaterialDto
            {
                Id = x.Id,
                Name = x.Name,
                UnitsRev = x.UnitsRev,
                Price = x.Price,
            });

            return new ParginatedListDto<MaterialDto>
            {
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.Items,
            };
        }

        /// <summary>
        /// Находит и возвращает материал, если он есть, который имеет указанный <paramref name="materialId"/>.
        /// </summary>
        /// <param name="materialId">Идентификатор материала для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<MaterialDto> Read(int materialId)
        {
            var material = await _repository.GetByIdAsync<Material>(materialId);
            return new MaterialDto
            {
                Id = material.Id,
                Name = material.Name,
                UnitsRev = material.UnitsRev,
                Price = material.Price,
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="materialDto"/> в системе.
        /// </summary>
        /// <param name="materialDto">Материал для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(MaterialDto materialDto)
        {
            var material = await _repository.GetByIdAsync<Material>(materialDto.Id);

           material.Name = materialDto.Name;
           material.UnitsRev = materialDto.UnitsRev;
           material.Price = materialDto.Price;

            _repository.Update(material);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет заказчика по идентификатору из системы.
        /// </summary>
        /// <param name="id">Идентификатор заказчика для удаления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpDelete]
        public async Task Delete(int id)
        {
            var material = await _repository.GetByIdAsync<Material>(id);
            _repository.Remove(material);
            await _repository.SaveChangesAsync();
        }
    }
}
