﻿using Microsoft.AspNetCore.Components;

static class ComponentReader
{
    public static IComponent? GetInstance(IRenderedFragment fragment)
    {
        var type = fragment.GetType();
        if (!type.IsGenericType)
        {
            return null;
        }

        var renderComponentInterface = type
            .GetInterfaces()
            .SingleOrDefault(_ =>
                _.IsGenericType &&
                _.GetGenericTypeDefinition() == typeof(IRenderedComponentBase<>));

        if (renderComponentInterface == null)
        {
            return null;
        }

        var instanceProperty = renderComponentInterface.GetProperty("Instance")!;
        return (IComponent) instanceProperty.GetValue(fragment)!;
    }
}