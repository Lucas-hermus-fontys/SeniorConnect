using System.Collections.Generic;
using System.Data;

namespace Domain.Interface;

public interface IDatabase
{
    public DataTable ExecuteQuery(string query, params object[] parameters);
    public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null);
}