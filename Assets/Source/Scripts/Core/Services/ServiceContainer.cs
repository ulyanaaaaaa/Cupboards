using System;
using System.Collections.Generic;

public static class ServiceContainer
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<T>(T service) where T : class => _services[typeof(T)] = service;
    public static T Resolve<T>() where T : class => _services.TryGetValue(typeof(T), out var s) ? s as T : null;
}