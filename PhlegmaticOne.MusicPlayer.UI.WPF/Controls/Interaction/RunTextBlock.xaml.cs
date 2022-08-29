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

    public static readonly DependencyProperty IsAlignToCenterIfWidthLessThanActualWidthProperty = DependencyProperty.Register(
        nameof(IsAlignToCenterIfWidthLessThanActualWidth), typeof(bool), typeof(RunTextBlock), new PropertyMetadata(default(bool)));

    public bool IsAlignToCenterIfWidthLessThanActualWidth
    {
        get => (bool) GetValue(IsAlignToCenterIfWidthLessThanActualWidthProperty);
        set => SetValue(IsAlignToCenterIfWidthLessThanActualWidthProperty, value);
    }

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
        if (animation is not null)
        {
            Block.BeginAnimation(Canvas.LeftProperty, animation);
        }
        else
        {
            var marginLeft = IsAlignToCenterIfWidthLessThanActualWidth ? (TextCanvas.ActualWidth - Block.ActualWidth) / 2 : 0;
            Block.SetValue(TextBlock.MarginProperty, new Thickness(marginLeft, Margin.Top, Margin.Right, Margin.Bottom));
        }
    }
}