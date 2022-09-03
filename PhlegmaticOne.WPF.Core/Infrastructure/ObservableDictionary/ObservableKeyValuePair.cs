using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhlegmaticOne.WPF.Core.Infrastructure.ObservableDictionary;

public class ObservableKeyValuePair<TKey, TValue> : INotifyPropertyChanged
{
    private TKey _key;
    private TValue _value;

    public ObservableKeyValuePair(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }

    public ObservableKeyValuePair() { }

    public TKey Key
    {
        get => _key;
        set
        {
            _key = value;
            OnPropertyChanged();
        }
    }


    public TValue Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}