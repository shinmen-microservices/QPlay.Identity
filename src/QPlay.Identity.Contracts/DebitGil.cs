using System;

namespace QPlay.Identity.Contracts;

/// <summary>
/// Represents a debit transaction of Gil (currency) from a user's account.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
/// <param name="Gil">The amount of Gil debited from the user's account.</param>
/// <param name="CorrelationId">The unique identifier associated with this debit transaction.</param>
public record DebitGil(Guid UserId, decimal Gil, Guid CorrelationId);