using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace QPlay.Identity.Service.Models.Entities;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    public decimal Gil { get; set; }
    public HashSet<Guid> MessageIds { get; set; } = new();
}