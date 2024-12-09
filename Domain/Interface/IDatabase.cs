using System.Collections.Generic;
using System.Data;

namespace Domain.Interface
{
    public interface IDatabase
    {
        DataTable ExecuteQuery(string query, params object[] parameters);
        int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null);
        public List<T> ExecuteQueryAndMap<T>(string query, List<object> parameters) where T : new();

        public List<T> ExecuteQueryAndMap<T>(string query, List<int> parameters) where T : new();
    }
}