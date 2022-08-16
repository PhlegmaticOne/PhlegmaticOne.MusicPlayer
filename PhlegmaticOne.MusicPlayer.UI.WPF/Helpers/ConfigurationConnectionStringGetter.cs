using System.Configuration;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

public class ConfigurationConnectionStringGetter : IConnectionStringGetter
{
    public string GetConnectionString() => 
        ConfigurationManager.ConnectionStrings["sql-connection-string"].ConnectionString;
}