using System.Data;

namespace Domain.Interface;

public interface IDatabaseCommand
{
    string CommandText { get; set; }
    void AddParameter(string name, object value);
    IDataReader ExecuteReader();
}