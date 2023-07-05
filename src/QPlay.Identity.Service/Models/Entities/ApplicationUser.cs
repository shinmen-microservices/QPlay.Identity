using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace QPlay.Identity.Service.Models.Entities;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    public decimal Gil { get; set; }
}