using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.MediatrConfig;

public interface IMediatorServiceTypeConverter
{
    Type Convert(Type sourceType, ConverterDelegate next);
}