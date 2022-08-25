using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ContentChangeableControl
{
    public static readonly DependencyProperty DefaultContentProperty = DependencyProperty.Register(
        nameof(DefaultContent), typeof(object), typeof(ContentChangeableControl), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty ContentToChangeProperty = DependencyProperty.Register(
        nameof(ContentToChange), typeof(object), typeof(ContentChangeableControl), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty IsContentChangingProperty = DependencyProperty.Register(
        nameof(IsContentChanging), typeof(bool), typeof(ContentChangeableControl), new PropertyMetadata(default(bool)));

    public bool IsContentChanging
    {
        get => (bool) GetValue(IsContentChangingProperty);
        set => SetValue(IsContentChangingProperty, value);
    }
    public object ContentToChange
    {
        get => GetValue(ContentToChangeProperty);
        set => SetValue(ContentToChangeProperty, value);
    }
    public object DefaultContent
    {
        get => GetValue(DefaultContentProperty);
        set => SetValue(DefaultContentProperty, value);
    }
    public ContentChangeableControl()
    {
        InitializeComponent();
    }
}