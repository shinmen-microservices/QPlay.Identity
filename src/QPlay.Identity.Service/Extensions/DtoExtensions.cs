using QPlay.Identity.Service.Models.Dtos;
using QPlay.Identity.Service.Models.Entities;

namespace QPlay.Identity.Service.Extensions;

public static class DtoExtensions
{
    public static UserDto AsDto(this ApplicationUser user)
    {
        return new UserDto
        (
            user.Id,
            user.UserName,
            user.Email,
            user.Gil,
            user.CreatedOn
        );
    }
}
