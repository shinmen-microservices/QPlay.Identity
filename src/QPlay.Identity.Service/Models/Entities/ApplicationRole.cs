using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace QPlay.Identity.Service.Models.Entities;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid> { }