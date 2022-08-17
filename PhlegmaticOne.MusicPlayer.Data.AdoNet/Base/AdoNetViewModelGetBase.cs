using System.Data;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public abstract class AdoNetViewModelGetBase<T> : ViewModelGetBase<T> where T: EntityBaseViewModel, IEntityCollection
{
    private readonly string _commandName;
    private readonly SqlConnection _connection;

    protected AdoNetViewModelGetBase(ISqlClient sqlClient, string commandName)
    {
        _commandName = commandName;
        _connection = sqlClient.GetConnection();
    }
    public override async Task<T> GetAsync()
    {
        await using var command = new SqlCommand(_commandName, _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        await using var reader = await command.ExecuteReaderAsync();
        return await Create(reader);
    }
    protected abstract Task<T> Create(SqlDataReader reader);
}