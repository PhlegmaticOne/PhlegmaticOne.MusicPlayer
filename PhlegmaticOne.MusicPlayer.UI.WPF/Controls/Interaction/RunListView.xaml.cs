using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

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
        var animation = AnimationsGenerator.GenerateSlideAnimation(textBlock, ListView, SlideTime);
        textBlock.BeginAnimation(Canvas.LeftProperty, animation);
    }
}