using EstimateResolve.Entities;
using Npgsql;

namespace EstimateResolve.Repositories
{
    interface IClientRepository
    {
        /// <summary>
        /// Получает всех заказчиков из бызы данных.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Client>> GetAll();

        /// <summary>
        /// Получает строку заказчика по заданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказчика.</param>
        /// <returns></returns>
        Task<Client> Get(int id);

        /// <summary>
        /// Добавляет нового заказчика
        /// </summary>
        /// <param name="client">Заказчик для добавляения</param>
        /// <returns></returns>
        Task Add(Client client);

        /// <summary>
        /// Изменение строки таблицы по выбранному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор строки заказчика.</param>
        /// <param name="client">Измененная версия для сохранения</param>
        /// <returns></returns>
        Task Update(int id, Client client);

        /// <summary>
        /// Удаление данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task Delete(int id);
    }

    public class ClientRepository : IClientRepository
    {
        private const string ConnectionString = @"Host=localhost:5432;
Username=postgres;
Password=admin;
Database=Estimate";

        private const string TableName = "client";

        private readonly NpgsqlConnection _connection;
        public ClientRepository()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();
        }
        public async Task<IEnumerable<Client>> GetAll()
        {
            var entities = new List<Client>();

            string commandText = $"SELECT * FROM {TableName}";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Client client = ReadClient(reader);
                        entities.Add(client);
                    }
            }
            return entities;

        }

        private static Client ReadClient(NpgsqlDataReader reader)
        {
            int? id = reader["id"] as int?;
            string name = reader["name"] as string;

            Client client = new Client
            {
                Id = id.Value,
                Name = name
            };

            return client;
        }

        public async Task<Client> Get(int id)
        {
            string commandText = $"SELECT * FROM {TableName} WHERE id = (@p)";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("p", id);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        return ReadClient(reader);
                    }
            }
            return null;
        }

        public async Task Add(Client client)
        {
            string commandText = $"INSERT INTO {TableName} (name) VALUES (@name)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("name", client.Name);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(int id, Client client)
        {
            var commandText = $@"UPDATE {TableName}
                SET name = @name
                WHERE id = @id";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", client.Name);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Delete(int id)
        {
            string commandText = $"DELETE FROM {TableName} WHERE id = (@p)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("p", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
