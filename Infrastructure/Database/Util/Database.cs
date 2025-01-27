﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Domain.Interface;
using Domain.Util;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database.Util

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
        
        public List<T> ExecuteQueryAndMap<T>(string query, List<object> parameters) where T : new()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand())
                {
                    mySqlCommand.Connection = connection;
        
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        int index = query.IndexOf('?');
                        if (index < 0) break;

                        query = query.Remove(index, 1).Insert(index, $"@param{i}");
                    }

                    mySqlCommand.CommandText = query;

                    if (parameters.Count > 0)
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            mySqlCommand.Parameters.AddWithValue($"@param{i}", parameters[i]);
                        }
                    }

                    using (IDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        var results = new List<T>();
                        while (reader.Read())
                        {
                            results.Add(MapToObject<T>(reader));
                        }
                        return results;
                    }
                }
            }
        }
        
        public List<T> ExecuteQueryAndMap<T>(string query, List<int> parameters) where T : new()
        {
            List<object> objectParameters = parameters.Cast<object>().ToList();
    
            return ExecuteQueryAndMap<T>(query, objectParameters);
        }

        public List<T> ExecuteQueryAndMap<T>(string query) where T : new()
        {
            return ExecuteQueryAndMap<T>(query, new List<object>());
        }
        
        private T MapToObject<T>(IDataRecord record) where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                string snakeCaseName = property.Name.ToSnakeCase();
                if (record.HasColumn(snakeCaseName) && !record.IsDBNull(record.GetOrdinal(snakeCaseName)))
                {
                    var value = record[snakeCaseName];
                    if (property.PropertyType.IsEnum)
                    {
                        string enumTypeName = $"{typeof(T).Name}{property.Name}";
                        var enumType = Type.GetType("Domain.Enum." + enumTypeName +  ", Domain");
                        if (enumType != null)
                        {
                            value = EnumExtensions.ParseEnum(enumType, value.ToString());
                        }
                    }
                    property.SetValue(obj, value);
                }
            }

            return obj;
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