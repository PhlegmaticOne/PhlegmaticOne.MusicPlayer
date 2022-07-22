using System;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF;

public partial class MainWindow
{
    private readonly ApplicationDbContext _dbContext;
    public INavigator Navigator { get; set; } = new Navigator();
    public MainWindow(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        InitializeComponent();
        App.LanguageChanged += AppOnLanguageChanged;
        DataContext = this;
    }

    private void AppOnLanguageChanged(object? sender, EventArgs e)
    {

    }
}