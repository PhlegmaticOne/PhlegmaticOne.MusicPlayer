using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class MoveButton
{
    public static readonly DependencyProperty ContentToShowProperty = DependencyProperty.Register(
        nameof(ContentToShow), typeof(object), typeof(MoveButton), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty CanMoveProperty = DependencyProperty.Register(
        nameof(CanMove), typeof(bool), typeof(MoveButton), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty MoveCommandProperty = DependencyProperty.Register(
        nameof(MoveCommand), typeof(ICommand), typeof(MoveButton), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty ActiveColorProperty = DependencyProperty.Register(
        nameof(ActiveColor), typeof(Brush), typeof(MoveButton), new PropertyMetadata(default(Brush)));

    public static readonly DependencyProperty NotActiveColorProperty = DependencyProperty.Register(
        nameof(NotActiveColor), typeof(Brush), typeof(MoveButton), new PropertyMetadata(default(Brush)));

    public Brush NotActiveColor
    {
        get => (Brush) GetValue(NotActiveColorProperty);
        set => SetValue(NotActiveColorProperty, value);
    }
    public Brush ActiveColor
    {
        get => (Brush) GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }
    public ICommand MoveCommand
    {
        get => (ICommand) GetValue(MoveCommandProperty);
        set => SetValue(MoveCommandProperty, value);
    }
    public bool CanMove
    {
        get => (bool) GetValue(CanMoveProperty);
        set => SetValue(CanMoveProperty, value);
    }
    public object ContentToShow
    {
        get => GetValue(ContentToShowProperty);
        set => SetValue(ContentToShowProperty, value);
    }
    public MoveButton()
    {
        InitializeComponent();
    }
}