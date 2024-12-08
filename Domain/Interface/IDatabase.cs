using System.Collections.Generic;
using System.Data;

namespace Domain.Interface
{
    public interface IDatabase
    {
        DataTable ExecuteQuery(string query, params object[] parameters);
        int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null);
        List<T> ExecuteQueryWithCommand<T>(IDatabaseCommand command, string query, params object[] parameters) where T : new();
    }
}