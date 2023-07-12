using System;

namespace QPlay.Identity.Service.Exceptions;

/// <summary>
/// Exception thrown when an unknown user is encountered.
/// </summary>
[Serializable]
public class UnknownUserException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownUserException"/> class with the specified user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the unknown user.</param>
    public UnknownUserException(Guid userId)
        : base($"Unknown user '{userId}'")
    {
        UserId = userId;
    }

    /// <summary>
    /// Gets the unique identifier of the unknown user.
    /// </summary>
    public Guid UserId { get; }
}