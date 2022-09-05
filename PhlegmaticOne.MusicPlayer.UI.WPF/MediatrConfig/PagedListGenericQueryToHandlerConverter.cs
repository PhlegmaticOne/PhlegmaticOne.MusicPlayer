using System;
using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Handlers;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.MediatrConfig;

public class PagedListGenericQueryToHandlerConverter : IMediatorServiceTypeConverter
{
    public Type Convert(Type sourceType, ConverterDelegate next)
    {
        var isRequestHandler = sourceType.IsGenericType &&
                               sourceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
        if (!isRequestHandler) return next();

        var requestType = sourceType.GenericTypeArguments[0];
        var shouldConvertType = requestType.IsGenericType &&
                                requestType.GetGenericTypeDefinition() == typeof(GenericGetPagedListQuery<>);
        if (!shouldConvertType) return next();
        var returnType = requestType.GenericTypeArguments[0];
        return typeof(GenericGetPagedListQueryHandler<>).MakeGenericType(returnType);
    }
}

public class EntitiesCountGenericQueryToHandlerConverter : IMediatorServiceTypeConverter
{
    public Type Convert(Type sourceType, ConverterDelegate next)
    {
        var isRequestHandler = sourceType.IsGenericType &&
                               sourceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
        if (!isRequestHandler) return next();

        var requestType = sourceType.GenericTypeArguments[0];
        var shouldConvertType = requestType.IsGenericType &&
                                requestType.GetGenericTypeDefinition() == typeof(GenericGetEntitiesCountQuery<>);
        if (!shouldConvertType) return next();
        var returnType = requestType.GenericTypeArguments[0];
        return typeof(GenericGetEntitiesCountQueryHandler<>).MakeGenericType(returnType);
    }
}