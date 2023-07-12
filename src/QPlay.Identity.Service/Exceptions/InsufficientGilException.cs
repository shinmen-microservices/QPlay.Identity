using System;

namespace QPlay.Identity.Service.Exceptions;

/// <summary>
/// Exception thrown when there is insufficient Gil (currency) to perform a debit transaction.
/// </summary>
[Serializable]
public class InsufficientGilException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InsufficientGilException"/> class with the specified user identifier and the amount of Gil to debit.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="gilToDebit">The amount of Gil to debit.</param>
    public InsufficientGilException(Guid userId, decimal gilToDebit)
        : base($"Not enough gil to debit {gilToDebit} from user '{userId}'")
    {
        UserId = userId;
        GilToDebit = gilToDebit;
    }

    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Gets the amount of Gil to debit.
    /// </summary>
    public decimal GilToDebit { get; }
}