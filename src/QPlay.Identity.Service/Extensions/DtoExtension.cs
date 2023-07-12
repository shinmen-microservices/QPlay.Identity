using QPlay.Identity.Service.Models.Dtos;
using QPlay.Identity.Service.Models.Entities;

namespace QPlay.Identity.Service.Extensions;

public static class DtoExtension
{
    public static UserDto AsDto(this ApplicationUser user)
    {
        return new(user.Id, user.UserName, user.Email, user.Gil, user.CreatedOn);
    }
}