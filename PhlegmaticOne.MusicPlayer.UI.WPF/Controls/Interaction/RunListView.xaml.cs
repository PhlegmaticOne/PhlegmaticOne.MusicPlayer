using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class RunListView
{
    public static readonly DependencyProperty ItemClickCommandProperty = DependencyProperty.Register(
        nameof(ItemClickCommand), typeof(ICommand), typeof(RunListView), new PropertyMetadata(default(ICommand)));
    public static readonly DependencyProperty ForegroundHoverColorProperty = DependencyProperty.Register(
        nameof(ForegroundHoverColor), typeof(Brush), typeof(RunListView), new PropertyMetadata(default(Brush)));
    public static readonly DependencyProperty SlideTimeProperty = DependencyProperty.Register(
        nameof(SlideTime), typeof(Duration), typeof(RunListView), new PropertyMetadata(default(Duration)));

    public ICommand ItemClickCommand
    {
        get => (ICommand) GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }
    public Brush ForegroundHoverColor
    {
        get => (Brush) GetValue(ForegroundHoverColorProperty);
        set => SetValue(ForegroundHoverColorProperty, value);
    }
    public Duration SlideTime
    {
        get => (Duration) GetValue(SlideTimeProperty);
        set => SetValue(SlideTimeProperty, value);
    }

    public RunListView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var textBlock = (TextBlock)ListView.Template.FindName("Block", ListView);
        var textBlockActualWidth = textBlock.ActualWidth;
        var listViewActualWidth = ListView.ActualWidth;

        if (listViewActualWidth >= textBlockActualWidth)
        {
            return;
        }

        var animation = CreateAnimation(listViewActualWidth - textBlockActualWidth - 10);

        textBlock.BeginAnimation(Canvas.LeftProperty, animation);
    }

    private DoubleAnimation CreateAnimation(double to)
    {
        var time = SlideTime == Duration.Automatic ? TimeSpan.FromSeconds(5) : SlideTime;
        return new()
        {
            From = 10,
            To = to,
            AutoReverse = true,
            Duration = time,
            AccelerationRatio = 0.1,
            DecelerationRatio = 0.1,
            RepeatBehavior = RepeatBehavior.Forever
        };
    }
}