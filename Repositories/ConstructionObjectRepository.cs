using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstimateResolve.Entities;
using Npgsql;

namespace EstimateResolve.Repositories
{
    interface IConstructionObject
    {
        /// <summary>
        /// Получает все объекты строительства из бызы данных.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ConstructionObject>> GetAll();

        /// <summary>
        /// Получает строку объекта по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns></returns>
        Task<ConstructionObject> Get(int id);

        /// <summary>
        /// Добавляет нового заказчика
        /// </summary>
        /// <param name="client">Заказчик для добавляения</param>
        /// <returns></returns>
        Task Add(ConstructionObject constructionObject);

        /// <summary>
        /// Изменение строки таблицы по выбранному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор строки объекта.</param>
        /// <param name="constructionObject">Измененная версия для сохранения</param>
        /// <returns></returns>
        Task Update(int id, ConstructionObject constructionObject);

        /// <summary>
        /// Удаление данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task Delete(int id);
    }

    public class ConstructionObjectRepository
    {
        private const string ConnectionString = @"Host=localhost:5432;
Username=postgres;
Password=admin;
Database=Estimate";


        private readonly NpgsqlConnection _connection;
        public ConstructionObjectRepository()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();
        }
        public async Task<IEnumerable<ConstructionObject>> GetAll()
        {
            var entities = new List<ConstructionObject>();

            string commandText = @"SELECT
            client.id AS client_id,
            client.name AS client_name,
            construction_object.id AS construction_object_id,
            construction_object.name AS construction_object_name
            FROM client
            INNER JOIN construction_object
            ON client.id = construction_object.client_id;";

            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        ConstructionObject constructionObject = ReadConstructionObject(reader);
                        entities.Add(constructionObject);
                    }
            }
            return entities;
        }
        private static ConstructionObject ReadConstructionObject(NpgsqlDataReader reader)
        {
            int? id = reader["construction_object_id"] as int?;
            int? clientId = reader["client_id"] as int?;
            string clientName = reader["client_name"] as string;
            string name = reader["construction_object_name"] as string;

            ConstructionObject constructionObject = new ConstructionObject
            {
                Id = id.Value,
                ClientId = clientId.Value,
                Client = new Client
                {
                    Id = clientId.Value,
                    Name = clientName
                },
                Name = name
            };

            return constructionObject;
        }
        public async Task<ConstructionObject> Get(int id)
        {
            string commandText = @"SELECT
            client.id AS client_id,
            client.name AS client_name,
            construction_object.id AS construction_object_id,
            construction_object.name AS construction_object_name
            FROM client
            INNER JOIN construction_object
            ON client.id = construction_object.client_id
            WHERE construction_object.id = (@p);";

            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("p", id);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        return ReadConstructionObject(reader);
                    }
            }
            return null;

        }
        public async Task Add(ConstructionObject constructionObject)
        {
            string commandText = "INSERT INTO construction_object (client_id, name) VALUES (@client_id,@name)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("name", constructionObject.Name);
                cmd.Parameters.AddWithValue("client_id", constructionObject.Id);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task Update(int id, ConstructionObject constructionObject)
        {
            var commandText = @"UPDATE construction_object
                SET client_id = @client_id, name = @name
                WHERE id = @id";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("client_id", constructionObject.ClientId);
                cmd.Parameters.AddWithValue("name", constructionObject.Name);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task Delete(int id)
        {
            string commandText = "DELETE FROM construction_object WHERE id = (@p)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("p", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }

}
