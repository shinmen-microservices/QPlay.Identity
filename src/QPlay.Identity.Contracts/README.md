# QPlay.Identity.Contracts

QPlay.Identity.Contracts is a class library that provides contract definitions for messages used to publish events on the queue. It is intended to be used in conjunction with the QPlay system.

## Usage

### DebitGil

The `DebitGil` record represents a debit transaction of Gil (currency) from a user's account. It contains the following properties:

- `UserId` (Guid): The unique identifier of the user.
- `Gil` (decimal): The amount of Gil debited from the user's account.
- `CorrelationId` (Guid): The unique identifier associated with this debit transaction.

### GilDebited

The `GilDebited` record represents a notification that a debit transaction of Gil has been successfully processed. It contains the following property:

- `CorrelationId` (Guid): The unique identifier associated with the original debit transaction.

### UserUpdated

The `UserUpdated` record represents an update to a user's information and their new total amount of Gil. It contains the following properties:

- `UserId` (Guid): The unique identifier of the user.
- `Email` (string): The updated email address of the user.
- `NewTotalGil` (decimal): The new total amount of Gil for the user's account.

To publish a message on the queue, you can create an instance of the appropriate record and send it to the messaging system using the provided methods.

## Examples

Here are some examples of how to use the QPlay.Identity.Contracts library:

```csharp
// DebitGil example
var debitTransaction = new DebitGil(userId, gilAmount, correlationId);
messagePublisher.Publish(debitTransaction);

// GilDebited example
var gilDebitedNotification = new GilDebited(correlationId);
messagePublisher.Publish(gilDebitedNotification);

// UserUpdated example
var userUpdate = new UserUpdated(userId, newEmail, newTotalGil);
messagePublisher.Publish(userUpdate);
```
