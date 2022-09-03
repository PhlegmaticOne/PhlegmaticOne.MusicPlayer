using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.MediatrConfig;

public static class MediatrServiceFactory
{
    public static ServiceFactory Wrap(ServiceFactory serviceFactory,
        IEnumerable<IMediatorServiceTypeConverter> converters)
    {
        return serviceType =>
        {
            Type NoConversion() => serviceType;
            var convertServiceType = converters
                .Reverse()
                .Aggregate((ConverterDelegate)NoConversion, (next, c) => () => c.Convert(serviceType, next));
            return serviceFactory(convertServiceType());
        };
    }
}