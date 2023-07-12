using GreenPipes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using QPlay.Common.MassTransit;
using QPlay.Common.Settings;
using QPlay.Identity.Service.Exceptions;
using QPlay.Identity.Service.Models.Entities;
using QPlay.Identity.Service.Settings;
using System;

namespace QPlay.Identity.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureApplicationUser(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        ServiceSettings serviceSettings = configuration
            .GetSection(nameof(ServiceSettings))
            .Get<ServiceSettings>();
        MongoDBSettings mongoDBSettings = configuration
            .GetSection(nameof(MongoDBSettings))
            .Get<MongoDBSettings>();

        services
            .Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)))
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                mongoDBSettings.ConnectionString,
                serviceSettings.ServiceName
            );

        return services;
    }

    public static IServiceCollection ConfigureIdentityServer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        IdentityServerSettings identityServerSettings = configuration
            .GetSection(nameof(IdentityServerSettings))
            .Get<IdentityServerSettings>();

        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddInMemoryApiScopes(identityServerSettings.ApiScopes)
            .AddInMemoryApiResources(identityServerSettings.ApiResources)
            .AddInMemoryClients(identityServerSettings.Clients)
            .AddInMemoryIdentityResources(identityServerSettings.IdentityResources);

        services.AddLocalApiAuthentication();
        return services;
    }

    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
    {
        services.AddMassTransitWithRabbitMq(retryConfigurator =>
        {
            retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
            retryConfigurator.Ignore(typeof(UnknownUserException));
            retryConfigurator.Ignore(typeof(InsufficientGilException));
        });

        return services;
    }
}