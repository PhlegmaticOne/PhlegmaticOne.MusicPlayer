using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Views;

public partial class Player
{
    private bool _isInited;
    private Canvas _thumbCanvas;
    private Border _thumbBorder;
    private readonly TranslateTransform _mouseOverTransform;
    private readonly TranslateTransform _mouseLeaveTransform;
    public Player()
    {
        InitializeComponent();
        _mouseOverTransform = new TranslateTransform(-3, -5);
        _mouseLeaveTransform = new TranslateTransform(-3, 0);
        PlayerLine.MouseEnter += PlayerLineOnMouseEnter;
        PlayerLine.MouseLeave += PlayerLineOnMouseLeave;
        VolumeButton.MouseEnter += VolumeButtonOnMouseEnter;
    }

    private async void VolumeButtonOnMouseEnter(object sender, MouseEventArgs e)
    {
        await Task.Delay(500);
        var volumeButton = sender as Button;
        if (volumeButton.IsMouseOver)
        {
            VolumePopup.IsOpen = true;
        }
    }

    private void PlayerLineOnMouseLeave(object sender, MouseEventArgs e)
    {
        if (_isInited == false)
        {
            Init(sender as Slider);
        }

        _thumbBorder.Height = 7;
        _thumbCanvas.RenderTransform = _mouseLeaveTransform;
    }

    private void PlayerLineOnMouseEnter(object sender, MouseEventArgs e)
    {
        if (_isInited == false)
        {
            Init(sender as Slider);
        }
        _thumbBorder.Height = 20;
        _thumbCanvas.RenderTransform = _mouseOverTransform;
    }

    private void Init(Slider slider)
    {
        var sliderTemplate = slider.Template;
        var thumb = (Thumb)sliderTemplate.FindName("Thumb", PlayerLine);
        var thumbTemplate = thumb.Template;
        _thumbCanvas = (Canvas)thumbTemplate.FindName("ThumbCanvas", thumb);
        _thumbBorder = (Border)thumbTemplate.FindName("ThumbBorder", thumb);
        _isInited = true;
    }
}