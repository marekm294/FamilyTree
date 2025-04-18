﻿using Api.Controllers.Abstraction;
using Api.FilterAttributes;
using Domain.DataServices.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using Shared.QueryArgs;

namespace Api.Controllers;

public class FamilyMemberController : BaseController
{
    [HttpPost]
    [RequiredFilter("createFamilyMemberInput")]
    [ValidationFilter<CreateFamilyMemberInput>()]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FamilyMemberOutput>> CreateFamilyMemberAsync(
        [FromBody] CreateFamilyMemberInput createFamilyMemberInput,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMemberOutput = await familyMemberService.CreateFamilyMemberAsync(createFamilyMemberInput);
        return Created("", familyMemberOutput);
    }

    [HttpPut]
    [RequiredFilter("updateFamilyMemberInput")]
    [ValidationFilter<UpdateFamilyMemberInput>()]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FamilyMemberOutput>> UpdateFamilyMemberAsync(
        [FromBody] UpdateFamilyMemberInput updateFamilyMemberInput,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMemberOutput = await familyMemberService
            .UpdateFamilyMemberAsync(updateFamilyMemberInput);
        return Ok(familyMemberOutput);
    }

    [HttpDelete]
    [RequiredFilter("deleteQueryArgs")]
    [ValidationFilter<DeleteQueryArgs>()]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FamilyMemberOutput>> DeleteFamilyMemberAsync(
        [FromQuery] DeleteQueryArgs deleteQueryArgs,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        await familyMemberService
            .DeleteFamilyMemberAsync(deleteQueryArgs);
        return NoContent();
    }
}