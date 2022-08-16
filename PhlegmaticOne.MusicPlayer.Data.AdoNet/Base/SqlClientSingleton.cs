using System.Data;
using Microsoft.Data.SqlClient;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public class SqlClientSingleton : ISqlClient
{
    private readonly SqlConnection _sqlConnection;
    public SqlClientSingleton(IConnectionStringGetter connectionStringGetter)
    {
        _sqlConnection = new SqlConnection(connectionStringGetter.GetConnectionString());
    }

    public SqlConnection GetConnection()
    {
        if (_sqlConnection.State == ConnectionState.Closed)
        {
            _sqlConnection.Open();
        }

        return _sqlConnection;
    }

    public void Dispose()
    {
        _sqlConnection.Dispose();
    }
}