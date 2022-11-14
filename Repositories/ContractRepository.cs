using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstimateResolve.Entities;
using Npgsql;

namespace EstimateResolve.Repositories
{
    interface IContractRepository
    {
        /// <summary>
        /// Получает все  из бызы данных.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contract>> GetAll();

        /// <summary>
        /// Получает строку договора по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор договора.</param>
        /// <returns></returns>
        Task<Contract> Get(int id);

        /// <summary>
        /// Добавляет новый договор.
        /// </summary>
        /// <param name="сontract">Договор для добавляения.</param>
        /// <returns></returns>
        Task Add(Contract сontract);

        /// <summary>
        /// Изменение строки таблицы по выбранному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор строки .</param>
        /// <param name="сontract">Измененная версия для сохранения</param>
        /// <returns></returns>
        Task Update(int id, Contract сontract);

        /// <summary>
        /// Удаление данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns></returns>
        Task Delete(int id);

    }

    public class ContractRepository : IContractRepository
    {

        private const string ConnectionString = @"Host=localhost:5432;
Username=postgres;
Password=admin;
Database=Estimate";

        private const string TableName = "contract";

        private readonly NpgsqlConnection _connection;
        public ContractRepository()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();
        }

        public async Task<IEnumerable<Contract>> GetAll()
        {
            var entities = new List<Contract>();

            string commandText = @"SELECT
            client.id AS client_id,
            client.name AS client_name,
            contract.id AS contract_id,
            contract.name AS contract_name
            FROM client
            INNER JOIN contract
            ON client.id = contract.client_id;";

            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Contract contract = ReadContract(reader);
                        entities.Add(contract);
                    }
            }
            return entities;

        }

        private static Contract ReadContract(NpgsqlDataReader reader)
        {
            int? id = reader["contract_id"] as int?;
            string name = reader["contract_name"] as string;
            int? clientId = reader["client_id"] as int?;
            string clientName = reader["client_name"] as string;


            Contract contract = new Contract
            {
                Id = id.Value,
                Name = name,
                Client = new Client
                {
                    Id = clientId.Value,
                    Name = clientName
                },
            };

            return contract;
        }

        public async Task<Contract> Get(int id)
        {
            string commandText = @"SELECT
            client.id AS client_id,
            client.name AS client_name,
            contract.id AS contract_id,
            contract.name AS contract_name
            FROM client
            INNER JOIN contract
            ON client.id = contract.client_id
            WHERE contract.id =@(p);";

            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("p", id);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        return ReadContract(reader);
                    }
            }
            return null;
        }

        public async Task Add(Contract contract)
        {
            string commandText = $"INSERT INTO contract (name, client_id) VALUES (@name,@client_id)";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("name", contract.Name);
                cmd.Parameters.AddWithValue("client_id", contract.ClientId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(int id, Contract contract)
        {
            var commandText = $@"UPDATE {TableName}
                SET name = @name, client_id=@client_id
                WHERE id = @id";
            await using (var cmd = new NpgsqlCommand(commandText, _connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("client_id", contract.ClientId);
                cmd.Parameters.AddWithValue("name", contract.Name);

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
