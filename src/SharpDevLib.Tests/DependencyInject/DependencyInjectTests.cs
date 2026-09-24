using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace SharpDevLib.Tests.DependencyInject;

[TestClass]
public class DependencyInjectTests
{
    [TestMethod]
    public void SingletonTest()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddImplementationsOf([Assembly.GetExecutingAssembly()!]);
        using var provider = services.BuildServiceProvider();

        using var scope1 = provider.CreateScope();
        var service1 = scope1.ServiceProvider.GetRequiredService<SingletonService>();

        using var scope2 = provider.CreateScope();
        var service2 = scope2.ServiceProvider.GetRequiredService<SingletonService>();
        var service3 = scope2.ServiceProvider.GetRequiredService<SingletonService>();

        Assert.AreEqual(service1.Id, service2.Id);
        Assert.AreEqual(service2.Id, service3.Id);
    }

    [TestMethod]
    public void ScopedTest()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddImplementationsOf([Assembly.GetExecutingAssembly()!]);
        using var provider = services.BuildServiceProvider();

        using var scope1 = provider.CreateScope();
        var service1 = scope1.ServiceProvider.GetRequiredService<ScopedService>();

        using var scope2 = provider.CreateScope();
        var service2 = scope2.ServiceProvider.GetRequiredService<ScopedService>();
        var service3 = scope2.ServiceProvider.GetRequiredService<ScopedService>();

        Assert.AreNotEqual(service1.Id, service2.Id);
        Assert.AreEqual(service2.Id, service3.Id);
    }

    [TestMethod]
    public void TransientTest()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddImplementationsOf([Assembly.GetExecutingAssembly()!]);
        using var provider = services.BuildServiceProvider();

        using var scope1 = provider.CreateScope();
        var service1 = scope1.ServiceProvider.GetRequiredService<TransientService>();

        using var scope2 = provider.CreateScope();
        var service2 = scope2.ServiceProvider.GetRequiredService<TransientService>();
        var service3 = scope2.ServiceProvider.GetRequiredService<TransientService>();

        Assert.AreNotEqual(service1.Id, service2.Id);
        Assert.AreNotEqual(service2.Id, service3.Id);
    }
}

public class SingletonService : ISingletonService
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public class ScopedService : IScopedService
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public class TransientService : ITransientService
{
    public Guid Id { get; set; } = Guid.NewGuid();
}