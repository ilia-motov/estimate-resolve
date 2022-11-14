using System.Security.Cryptography;
using EstimateResolve.Entities;
using Npgsql;

namespace EstimateResolve.Repositories
{
    public interface IUnitOfMeasurementRepository
    {
        /// <summary>
        /// Получает все единицы измерения из бызы данных.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UnitOfMeasurement>> GetAll();

        /// <summary>
        /// Получает единицу измерения по заданному идентификаторую
        /// </summary>
        /// <param name="id">Идентификатор единицы измерения.</param>
        /// <returns></returns>
        Task<UnitOfMeasurement> Get(int id);

        /// <summary>
        /// Добавляет новую единицу измерения
        /// </summary>
        /// <param name="unitOfMeasurement">единица измерения для добавления</param>
        /// <returns></returns>
        Task Add(UnitOfMeasurement unitOfMeasurement);

        /// <summary>
        /// Изменение строки таблицы по выбранному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор единицы измерения.</param>
        /// <param name="unitOfMeasurement">Измененная версия для сохранения</param>
        /// <returns></returns>
        Task Update(int id, UnitOfMeasurement unitOfMeasurement);

        /// <summary>
        /// Удаление данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task Delete(int id);
    }

    public class UnitOfMeasurementRepository : IUnitOfMeasurementRepository
    {
        private const string ConnectionString = @"Host=localhost:5432;
Username=postgres;
Password=admin;
Database=Estimate";

        private const string TableName = "unit_of_measurement";

        private readonly NpgsqlConnection _connection;

        public UnitOfMeasurementRepository()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();
        }

        public async Task<IEnumerable<UnitOfMeasurement>> GetAll()
        {
            var entities = new List<UnitOfMeasurement>();

            string commandText = $"SELECT * FROM {TableName}";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        UnitOfMeasurement unitOfMeasurement1 = ReadUnitOfMeasurement(reader);
                        entities.Add(unitOfMeasurement1);
                    }
            }
            return entities;
        }

        public async Task<UnitOfMeasurement> Get(int id)
        {
            string commandText = $"SELECT * FROM {TableName} WHERE id = @id";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        return ReadUnitOfMeasurement(reader);
                    }
            }

            return null;
        }

        public async Task Add(UnitOfMeasurement unitOfMeasurement)
        {
            string commandText = $"INSERT INTO {TableName} (name) VALUES (@name)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("name", unitOfMeasurement.Name);

                await cmd.ExecuteNonQueryAsync();
            }
        }


        public async Task Update(int id, UnitOfMeasurement unitOfMeasurement)
        {
            var commandText = $@"UPDATE {TableName}
                SET name = @name
                WHERE id = @id";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", unitOfMeasurement.Name);

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

        private static UnitOfMeasurement ReadUnitOfMeasurement(NpgsqlDataReader reader)
        {
            int? id = reader["id"] as int?;
            string name = reader["name"] as string;

            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement
            {
                Id = id.Value,
                Name = name
            };

            return unitOfMeasurement;
        }
    }
}
