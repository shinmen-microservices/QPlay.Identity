using System.ComponentModel.DataAnnotations;

namespace QPlay.Identity.Service.Models.Dtos;

public record UpdateUserDto
(
    [Required] [EmailAddress] string Email,
    [Range(0, 1000000)] decimal Gil
);