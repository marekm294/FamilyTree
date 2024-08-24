using Api.Controllers.Abstraction;
using Api.FilterAttributes;
using Domain.DataServices.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Inputs.Families;
using Shared.Models.Outputs;

namespace Api.Controllers;

public class FamilyController : BaseController
{
    [HttpPost]
    [RequiredFilter("createFamilyInput")]
    [ValidationFilter<CreateFamilyInput>()]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FamilyOutput>> CreateFamilyAsync(
        [FromBody] CreateFamilyInput createFamilyInput,
        [FromServices] IFamilyService familyService)
    {
        var familyOutput = await familyService.CreateFamilyAsync(createFamilyInput);
        return Created("", familyOutput);
    }

    [HttpPut]
    [RequiredFilter("updateFamilyInput")]
    [ValidationFilter<UpdateFamilyInput>()]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FamilyOutput>> UpdateFamilyOutputAsync(
        [FromBody] UpdateFamilyInput updateFamilyInput,
        [FromServices] IFamilyService familyService)
    {
        var familyOutput = await familyService.UpdateFamilyAsync(updateFamilyInput);
        return Ok(familyOutput);
    }

    [HttpGet]
    [ProducesResponseType<List<FamilyMemberOutput>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FamilyOutput>>> GetFamiliesAsync(
        [FromServices] IFamilyService familyService)
    {
        var families = await familyService.GetFamiliesAsync();
        return Ok(families);
    }

    [HttpGet("familyMembers/{familyId}")]
    [RequiredGuidFilter("familyId")]
    [ProducesResponseType<List<FamilyMemberOutput>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorOutput>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FamilyMemberOutput>>> GetFamilyMembersAsync(
        [FromRoute] Guid familyId,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMembers = await familyMemberService.GetFamilyMembersAsync(familyId);
        return Ok(familyMembers);
    }
}