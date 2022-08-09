using System.Diagnostics.CodeAnalysis;

namespace PhlegmaticOne.MusicPlayer.WPF.Core;

[AttributeUsage(AttributeTargets.Method)]
public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
{
    public NotifyPropertyChangedInvocatorAttribute() { }
    public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
    {
        ParameterName = parameterName;
    }

    [CanBeNull] public string ParameterName { get; }
}