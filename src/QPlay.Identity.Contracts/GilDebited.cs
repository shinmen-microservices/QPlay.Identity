using System;

namespace QPlay.Identity.Contracts;

/// <summary>
/// Represents a notification that a debit transaction of Gil has been successfully processed.
/// </summary>
/// <param name="CorrelationId">The unique identifier associated with the original debit transaction.</param>
public record GilDebited(Guid CorrelationId);