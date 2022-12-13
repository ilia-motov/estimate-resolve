using System.Collections.Generic;
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
    /// Предоставляет API для управления объектом строительства в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ConstructionObjectController : ControllerBase, IController<ConstructionObjectDto>
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="ConstructionObjectController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConstructionObjectController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Создает указанного <paramref name="сonstructionObject"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="сonstructionObject">Объект строительства для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPut]
        public async Task Create(ConstructionObjectDto сonstructionObjectDto)
        {
            var сonstructionObject = new ConstructionObject
            {
                Id = сonstructionObjectDto.Id,
                Name = сonstructionObjectDto.Name,
                ClientId = сonstructionObjectDto.Client.Id
            };

            _repository.Add(сonstructionObject);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список объектов строительства в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<ConstructionObjectDto>"/>
        /// операции.
        [HttpGet("[action]")]
        public async Task<ParginatedListDto<ConstructionObjectDto>> ReadAll(
            string searchString,
            string sortLabel,
            SortDirection sortDirection,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var specification = new PaginationSpecification<ConstructionObject>
            {
                Includes = x => x.Include(x => x.Client),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var paginatedList = await _repository.GetListAsync(specification, x => new ConstructionObjectDto
            {
                Id = x.Id,
                Name = x.Name,
                Client = new ClientDto
                {
                    Id= x.Client.Id,
                    Name = x.Client.Name
                }
            });

            return new ParginatedListDto<ConstructionObjectDto>
            {
                TotalItems = paginatedList.TotalItems,
                Items = paginatedList.Items,
            };
        }

        /// <summary>
        /// Находит и возвращает объект строительства, если он есть, который имеет указанный <paramref name="constructionObjectdId"/>.
        /// </summary>
        /// <param name="constructionObjectId">Идентификатор заказчика для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ConstructionObjectDto> Read(int constructionObjectId)
        {
            var specification = new Specification<ConstructionObject>
            {
                Conditions = new List<Expression<Func<ConstructionObject, bool>>>
                {
                    x => x.Id == constructionObjectId,
                },
                Includes = x => x.Include(x => x.Client)
            };

            var constructionObject = await _repository.GetAsync(specification);

            return new ConstructionObjectDto
            {
                Id = constructionObject.Id,
                Name = constructionObject.Name,
                Client = new ClientDto
                {
                    Id = constructionObject.Client.Id,
                    Name = constructionObject.Client.Name
                }
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="constructionObjectDto"/> в системе.
        /// </summary>
        /// <param name="constructionObjectDto">объект строительства для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(ConstructionObjectDto constructionObjectDto)
        {
            var constructionObject = await _repository.GetByIdAsync<ConstructionObject>(constructionObjectDto.Id);

            constructionObject.Name = constructionObjectDto.Name;
            constructionObject.ClientId = constructionObjectDto.Client.Id;

            _repository.Update(constructionObject);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет объект строительства по идентификатору из системы.
        /// </summary>
        /// <param name="id">объект строительства  для удаления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpDelete]
        public async Task Delete(int id)
        {
            var constructionObject = await _repository.GetByIdAsync<ConstructionObject>(id);
            _repository.Remove(constructionObject);
            await _repository.SaveChangesAsync();
        }
    }
}
