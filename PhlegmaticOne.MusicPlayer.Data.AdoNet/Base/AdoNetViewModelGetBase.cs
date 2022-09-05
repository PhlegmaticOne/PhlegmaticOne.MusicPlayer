using System.Data;
using Calabonga.UnitOfWork;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public abstract class AdoNetViewModelGetBase<T> : IEntityPagedListGet<T> where T: EntityBaseViewModel
{
    private readonly SqlConnection _connection;

    protected AdoNetViewModelGetBase(ISqlClient sqlClient)
    {
        _connection = sqlClient.GetConnection();
    }
    public async Task<IPagedList<T>> GetPagedListAsync(int pageSize, int pageIndex,
        Func<T, object>? sortFunc = null, Func<T, bool>? selectFunc = null)
    {
        await using var command = new SqlCommand(CommandName, _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        await using var reader = await command.ExecuteReaderAsync();
        return await Create(reader, pageSize, pageIndex);
    }
    protected abstract string CommandName { get; }
    protected abstract Task<IPagedList<T>> Create(SqlDataReader reader, int pageSize, int pageIndex,
        Func<T, object>? sortFunc = null, Func<T, bool>? selectFunc = null);
}