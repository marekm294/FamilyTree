using Api.Controllers.Abstraction;
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
    public async Task<ActionResult<FamilyMemberOutput>> CreateFamilyMemberAsync(
        [FromBody][Required] CreateFamilyMemberInput createFamilyMemberInput,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMemberOutput = await familyMemberService.CreateFamilyMemberAsync(createFamilyMemberInput);
        return Created("", familyMemberOutput);
    }

    [HttpPut]
    public async Task<ActionResult<FamilyMemberOutput>> UpdateFamilyMemberAsync(
        [FromBody][Required] UpdateFamilyMemberInput updateFamilyMemberInput,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMemberOutput = await familyMemberService
            .UpdateFamilyMemberAsync(updateFamilyMemberInput);
        return Ok(familyMemberOutput);
    }

    [HttpDelete]
    public async Task<ActionResult<FamilyMemberOutput>> DeleteFamilyMemberAsync(
        [FromQuery][Required] DeleteQueryArgs deleteQueryArgs,
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var isDeleted = await familyMemberService
            .DeleteFamilyMemberAsync(deleteQueryArgs);
        return NoContent();
    }
}