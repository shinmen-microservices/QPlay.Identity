﻿using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QPlay.Identity.Service.Extensions;
using QPlay.Identity.Service.Models.Dtos;
using QPlay.Identity.Service.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Duende.IdentityServer.IdentityServerConstants;

namespace QPlay.Identity.Service.Controllers;

[ApiController]
[Route("users")]
[Authorize(Policy = LocalApi.PolicyName)]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> Get()
    {
        IEnumerable<UserDto> users = userManager.Users.ToList().Select(user => user.AsDto());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetByIdAsync([FromRoute] Guid id)
    {
        ApplicationUser user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return NotFound();

        return user.AsDto();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateUserDto userDto)
    {
        ApplicationUser user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return NotFound();

        user.Email = userDto.Email;
        user.UserName = userDto.Email;
        user.Gil = userDto.Gil;

        await userManager.UpdateAsync(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        ApplicationUser user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return NotFound();

        await userManager.DeleteAsync(user);

        return NoContent();
    }
}

