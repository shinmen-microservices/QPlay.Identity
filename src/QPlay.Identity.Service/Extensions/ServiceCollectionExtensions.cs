using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using QPlay.Common.Settings;
using QPlay.Identity.Service.Models.Entities;
using System;

namespace QPlay.Identity.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationUser(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        ServiceSettings serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
        MongoDBSettings mongoDBSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
            (
                mongoDBSettings.ConnectionString,
                serviceSettings.ServiceName
            );

        return services;
    }
}
