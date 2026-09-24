using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 服务集合扩展方法
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 根据标记接口自动注册服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assembly">需要扫描的程序集</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddImplementationsOf(this IServiceCollection services, Assembly[] assembly)
    {
        return services
            .AddImplementationsOf<ISingletonService>(1, assembly)
            .AddImplementationsOf<IScopedService>(2, assembly)
            .AddImplementationsOf<ITransientService>(3, assembly);
    }

    /// <summary>
    /// 根据服务类型注册实现
    /// </summary>
    /// <typeparam name="TService">服务接口类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="type">服务生命周期类型（1=Singleton, 2=Scoped, 3=Transient）</param>
    /// <param name="assembly">需要扫描的程序集</param>
    /// <returns>服务集合</returns>
    static IServiceCollection AddImplementationsOf<TService>(this IServiceCollection services, int type, Assembly[] assembly)
    {
        var interfaceType = typeof(TService);
        var implementations = assembly
            .SelectMany(a => a.GetTypes())
            .Distinct()
            .Where(t => t.IsClass && !t.IsAbstract && interfaceType.IsAssignableFrom(t));

        foreach (var impl in implementations)
        {
            if (type == 1) services.AddSingleton(impl);
            else if (type == 2) services.AddScoped(impl);
            else if (type == 3) services.AddTransient(impl);
        }
        return services;
    }
}
