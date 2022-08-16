using Microsoft.Data.SqlClient;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public interface ISqlClient : IDisposable
{
    public SqlConnection GetConnection();
}