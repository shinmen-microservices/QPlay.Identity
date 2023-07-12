using System;

namespace QPlay.Identity.Service.Exceptions;

[Serializable]
internal class InsufficientGilException : Exception
{
    public InsufficientGilException(Guid userId, decimal gilToDebit)
        : base($"Not enough gil to debit {gilToDebit} from user '{userId}'")
    {
        UserId = userId;
        GilToDebit = gilToDebit;
    }

    public Guid UserId { get; }

    public decimal GilToDebit { get; }
}