using System.Data;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public abstract class AdoNetViewModelGetBase<T> : ViewModelGetBase<T> where T: EntityBaseViewModel, IEntityCollection
{
    private readonly SqlConnection _connection;

    protected AdoNetViewModelGetBase(ISqlClient sqlClient)
    {
        _connection = sqlClient.GetConnection();
    }
    public override async Task<T> GetAsync()
    {
        await using var command = new SqlCommand(CommandName, _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        await using var reader = await command.ExecuteReaderAsync();
        return await Create(reader);
    }
    protected abstract string CommandName { get; }
    protected abstract Task<T> Create(SqlDataReader reader);
}