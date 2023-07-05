using System;

namespace QPlay.Identity.Contracts;

public record UserUpdated
(
    Guid UserId,
    string Email,
    decimal NewTotalGil
);