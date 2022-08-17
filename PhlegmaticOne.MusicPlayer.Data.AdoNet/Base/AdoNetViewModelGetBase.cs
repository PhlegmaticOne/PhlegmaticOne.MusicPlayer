using System.Data;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public abstract class AdoNetViewModelGetBase<T> : ViewModelGetBase<T> where T: EntityBaseViewModel
{
    protected readonly SqlConnection Connection;
    protected readonly SqlCommand Command;

    protected AdoNetViewModelGetBase(ISqlClient sqlClient, string commandName)
    {
        Connection = sqlClient.GetConnection();
        Command = new SqlCommand(commandName, Connection)
        {
            CommandType = CommandType.StoredProcedure
        };
    }
    public override async Task<T> GetAsync(Guid id)
    {
        Command.Parameters.Clear();
        Command.Parameters.Add(new SqlParameter(ParameterName, SqlDbType.UniqueIdentifier)
        {
            Value = id
        });

        await using var reader = await Command.ExecuteReaderAsync();
        return await Create(reader, id);
    }

    protected abstract string ParameterName { get; }
    protected abstract Task<T> Create(SqlDataReader reader, Guid id);
}