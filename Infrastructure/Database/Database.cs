using System;
using System.Collections.Generic;
using System.Data;
using Domain.Interface;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database

{
    public class Database : IDatabase

    {
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable ExecuteQuery(string query, params object[] parameters)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = connection;

                for (int i = 0; i < parameters.Length; i++)
                {
                    int index = query.IndexOf('?');
                    if (index < 0) break;

                    query = query.Remove(index, 1).Insert(index, $"@param{i}");
                }

                command.CommandText = query;

                if (parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        command.Parameters.AddWithValue($"@param{i}", parameters[i]);
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