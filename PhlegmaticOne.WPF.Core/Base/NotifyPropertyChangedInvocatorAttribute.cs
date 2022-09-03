using System.Diagnostics.CodeAnalysis;

namespace PhlegmaticOne.WPF.Core.Base;

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