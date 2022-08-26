using System.Windows;
using System.Windows.Controls;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class RunTextBlock
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(RunTextBlock), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty SlideTimeProperty = DependencyProperty.Register(
        nameof(SlideTime), typeof(Duration), typeof(RunTextBlock), new PropertyMetadata(default(Duration)));

    public Duration SlideTime
    {
        get => (Duration) GetValue(SlideTimeProperty);
        set => SetValue(SlideTimeProperty, value);
    }
    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public RunTextBlock()
    {
        InitializeComponent();
        Block.SizeChanged += BlockOnSizeChanged;
    }

    private void BlockOnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (e.NewSize.Width == 0)
        {
            return;
        }
        SetupAnimation();
    }

    private void SetupAnimation()
    {
        var animation = AnimationsGenerator.GenerateSlideAnimation(Block, TextCanvas, SlideTime);
        Block.BeginAnimation(Canvas.LeftProperty, animation);
    }
}