using Api.Controllers.Abstraction;
using Api.FilterAttributes;
using Domain.DataServices.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using Shared.QueryArgs;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

public class FamilyMemberController : BaseController
{
    [HttpGet]
    // ToDelete
    public async Task<ActionResult<IEnumerable<FamilyMemberOutput>>> GetFamilyMembersAsync(
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMemberOutputs = await familyMemberService
            .GetAllFamilyMemberOutputsAsync();

        return Ok(familyMemberOutputs);
    }

    [HttpPost]
    
    [RequiredFilter("createFamilyMemberInput")]
    [ValidationFilter<CreateFamilyMemberInput>()]
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
    public async Task<ActionResult<FamilyMemberOutput>> DeleteFamilyMemberAsync(
        [FromQuery] DeleteQueryArgs deleteQueryArgs,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        await familyMemberService
            .DeleteFamilyMemberAsync(deleteQueryArgs);
        return NoContent();
    }
}