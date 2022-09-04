using System.Data;
using Calabonga.UnitOfWork;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

public abstract class AdoNetViewModelGetBase<T> : EntityPagedListGetBase<T> where T: EntityBaseViewModel
{
    private readonly SqlConnection _connection;

    protected AdoNetViewModelGetBase(ISqlClient sqlClient)
    {
        _connection = sqlClient.GetConnection();
    }
    public override async Task<IPagedList<T>> GetPagedListAsync(int pageSize, int pageIndex)
    {
        await using var command = new SqlCommand(CommandName, _connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        await using var reader = await command.ExecuteReaderAsync();
        return await Create(reader, pageSize, pageIndex);
    }
    protected abstract string CommandName { get; }
    protected abstract Task<IPagedList<T>> Create(SqlDataReader reader, int pageSize, int pageIndex);
}