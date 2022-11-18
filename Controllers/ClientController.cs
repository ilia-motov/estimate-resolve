﻿using EstimateResolve.DataTransferObjects;
using EstimateResolve.Entities;
using Microsoft.AspNetCore.Mvc;
using TanvirArjel.EFCore.GenericRepository;

namespace EstimateResolve.Controllers
{
    /// <summary>
    /// Предоставляет API для управления заказчиком в системе.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Создает новый экземляр <see cref="ClientController"/>.
        /// </summary>
        /// <param name="repository">Репозиторий сохраняемости, над которым будет работать контроллер.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ClientController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Создает указанного <paramref name="client"/> в системе,
        /// как асинхронная операция.
        /// </summary>
        /// <param name="client">Заказчик для создания.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
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

        /// <summary>
        /// Возвращает список заказчиков в системе, определенной страницы.
        /// </summary>
        /// <param name="pageIndex">Индекс страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <see cref="Task"/>, представляющий асинхронную операцию, содержащую <see cref="List<ClientDto>"/>
        /// операции.
        [HttpGet("[action]")]
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

        /// <summary>
        /// Находит и возвращает заказчика, если он есть, который имеет указанный <paramref name="cliendId"/>.
        /// </summary>
        /// <param name="cliendId">Идентификатор заказчика для чтения.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ClientDto> Read(int cliendId)
        {
            var client = await _repository.GetByIdAsync<Client>(cliendId);
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name
            };
        }

        /// <summary>
        /// Обновляет указанного <paramref name="clientDto"/> в системе.
        /// </summary>
        /// <param name="clientDto">Закзачик для обновления.</param>
        /// <returns>Объект <see cref="Task"/>, представляющий асинхронную операцию.</returns>
        [HttpPatch]
        public async Task Update(ClientDto clientDto)
        {
            var client = await _repository.GetByIdAsync<Client>(clientDto.Id);

            client.Id = clientDto.Id;
            client.Name = clientDto.Name;

            _repository.Update(client);
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
            var client = await _repository.GetByIdAsync<Client>(id);
            _repository.Remove(client);
            await _repository.SaveChangesAsync();
        }
    }
}
