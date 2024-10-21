using System.Data;
using MySql.Data.MySqlClient;

namespace SeniorConnect.Infrastructure

{
    public class Database
    {
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<String, object> param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        return result;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<String, object> param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}