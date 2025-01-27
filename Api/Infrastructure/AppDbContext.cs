namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Data;

    public class AppDbContext
    {
        private readonly string _connectionString;

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> ExecuteQueryAsync<T>(string query) where T : new()
        {
            var result = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(query, connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = MapReaderToObject<T>(reader);
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public async Task<List<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, Dictionary<string, object> parameters = null) where T : new()
        {
            var result = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = MapReaderToObject<T>(reader);
                        result.Add(item);
                    }
                }
            }

            return result;
        }


        private T MapReaderToObject<T>(SqlDataReader reader) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (reader.GetOrdinal(property.Name) >= 0 && reader[property.Name] != DBNull.Value)
                {
                    var propertyType = property.PropertyType;

                    if (Nullable.GetUnderlyingType(propertyType) != null)
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);
                    }
                    var value = Convert.ChangeType(reader[property.Name], propertyType);
                    property.SetValue(obj, value);
                }
            }

            return obj;
        }
    }
}
