using System;
using System.Data;
using Domain.Interface;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database.Util;

public class MySqlDatabaseCommand : IDatabaseCommand, IDisposable
{
    private readonly MySqlCommand _command;

    public MySqlDatabaseCommand()
    {
        _command = new MySqlCommand();
    }

    public string CommandText
    {
        get => _command.CommandText;
        set => _command.CommandText = value;
    }

    public MySqlConnection Connection
    {
        set => _command.Connection = value;
    }

    public void AddParameter(string name, object value)
    {
        _command.Parameters.AddWithValue(name, value);
    }

    public IDataReader ExecuteReader()
    {
        return _command.ExecuteReader();
    }
    
    public void Dispose()
    {
        _command?.Dispose();
    }
}