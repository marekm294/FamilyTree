using Api.Controllers.Abstraction;
using Domain.DataServices.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Outputs;

namespace Api.Controllers;

public class FamilyMemberController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FamilyMemberOutput>>> GetFamilyMembersAsync(
        [FromServices] IFamilyMemberService familyMemberService)
    {
        var familyMembers = await familyMemberService
            .GetAllFamilyMemberOutputsAsync();

        return Ok(familyMembers);
    }
}