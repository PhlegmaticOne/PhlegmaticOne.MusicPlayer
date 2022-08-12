namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

public class DisplayCultureInfo
{
    public DisplayCultureInfo(string displayName, string code)
    {
        DisplayName = displayName;
        Code = code;
    }

    public string DisplayName { get; set; }
    public string Code { get; set; }
}