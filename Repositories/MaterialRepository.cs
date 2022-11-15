using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstimateResolve.Entities;
using Npgsql;

namespace EstimateResolve.Repositories
{
    interface IMaterialRepository
    {
        /// <summary>
        /// Получает все материалы из бызы данных.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Material>> GetAll();

        /// <summary>
        /// Получает строку материала по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор материала.</param>
        /// <returns></returns>
        Task<Material> Get(int id);

        /// <summary>
        /// Добавляет новый материал.
        /// </summary>
        /// <param name="material">материал для добавляения.</param>
        /// <returns></returns>
        Task Add(Material material);

        /// <summary>
        /// Изменение строки таблицы по выбранному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор строки .</param>
        /// <param name="material">Измененная версия для сохранения</param>
        /// <returns></returns>
        Task Update(int id, Material material);

        /// <summary>
        /// Удаление данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task Delete(int id);

    }
    public class MaterialRepository : IMaterialRepository
    {
        private const string ConnectionString = @"Host=localhost:5432;
Username=postgres;
Password=admin;
Database=Estimate";

        private const string TableName = "material";

        private readonly NpgsqlConnection _connection;
        public MaterialRepository()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();
        }
        public async Task<IEnumerable<Material>> GetAll()
        {
            var entities = new List<Material>();

            string commandText = $"SELECT * FROM {TableName}";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Material material = ReadMaterial(reader);
                        entities.Add(material);
                    }
            }
            return entities;

        }
        private static Material ReadMaterial(NpgsqlDataReader reader)
        {
            int? id = reader["id"] as int?;
            string name = reader["name"] as string;
            string unitsRev = reader["units_rev"] as string;
            decimal? price = reader["price"] as decimal?;

            Material material = new Material
            {
                Id = id.Value,
                Name = name,
                UnitsRev = unitsRev,
                Price = price.Value,
            };

            return material;
        }

        public async Task<Material> Get(int id)
        {
            string commandText = $"SELECT * FROM {TableName} WHERE id = (@p)";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("p", id);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        return ReadMaterial(reader);
                    }
            }
            return null;
        }

        public async Task Add(Material material)
        {
            string commandText = $"INSERT INTO {TableName} (name,units_rev,price) VALUES (@name,@units_rev, @price)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("name", material.Name);
                cmd.Parameters.AddWithValue("units_rev", material.UnitsRev);
                cmd.Parameters.AddWithValue("price", material.Price);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task Update(int id, Material material)
        {
            var commandText = $@"UPDATE {TableName}
                SET name = @name, units_rev = @units_rev, price = @price
                WHERE id = @id";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", material.Name);
                cmd.Parameters.AddWithValue("units_rev", material.UnitsRev);
                cmd.Parameters.AddWithValue("price", material.Price);

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
