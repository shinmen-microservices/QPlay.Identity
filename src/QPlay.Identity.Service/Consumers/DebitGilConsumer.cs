using MassTransit;
using Microsoft.AspNetCore.Identity;
using QPlay.Identity.Contracts;
using QPlay.Identity.Service.Exceptions;
using QPlay.Identity.Service.Models.Entities;
using System.Threading.Tasks;

namespace QPlay.Identity.Service.Consumers;

public class DebitGilConsumer : IConsumer<DebitGil>
{
    private readonly UserManager<ApplicationUser> userManager;

    public DebitGilConsumer(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task Consume(ConsumeContext<DebitGil> context)
    {
        DebitGil message = context.Message;

        ApplicationUser user =
            await userManager.FindByIdAsync(message.UserId.ToString())
            ?? throw new UnknownUserException(message.UserId);

        if (user.MessageIds.Contains(context.MessageId.Value))
        {
            await context.Publish(new GilDebited(message.CorrelationId));
            return;
        }

        user.Gil -= message.Gil;

        if (user.Gil < 0)
        {
            throw new InsufficientGilException(message.UserId, message.Gil);
        }

        user.MessageIds.Add(context.MessageId.Value);

        await userManager.UpdateAsync(user);

        Task gitDebitedTask = context.Publish(new GilDebited(message.CorrelationId));
        Task userUpdatedTask = context.Publish(new UserUpdated(user.Id, user.Email, user.Gil));

        await Task.WhenAll(userUpdatedTask, gitDebitedTask);
    }
}