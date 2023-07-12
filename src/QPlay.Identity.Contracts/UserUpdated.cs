using System;

namespace QPlay.Identity.Contracts;

/// <summary>
/// Represents an update to a user's information and their new total amount of Gil.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="Email">The updated email address of the user.</param>
/// <param name="NewTotalGil">The new total amount of Gil for the user's account.</param>
public record UserUpdated(Guid UserId, string Email, decimal NewTotalGil);