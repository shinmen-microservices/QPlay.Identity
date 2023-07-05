using System;

namespace QPlay.Identity.Service.Models.Dtos;

public record UserDto
(
    Guid Id,
    string Username,
    string Email,
    decimal Gil,
    DateTimeOffset CreatedDate
);