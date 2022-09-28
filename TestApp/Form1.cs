using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using System.Drawing.Printing;

namespace TestApp;

public partial class Form1 : Form
{
    private readonly IEntityPagedListGet<AlbumPreviewViewModel> _entityPagedListGet;
    public Form1()
    {
        InitializeComponent();

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=music-player-db;Integrated Security=True;");

        var dbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
        _entityPagedListGet = new EFAllAlbumsViewModelGet(dbContext);
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        var items = await Task.Run(async () => await _entityPagedListGet.GetPagedListAsync(3, 0));
        foreach (var item in items.Items)
        {
            listBox1.Items.Add(item.Title);
        }
    }
}