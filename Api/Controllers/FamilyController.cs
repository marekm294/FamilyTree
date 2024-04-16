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
    public async Task<ActionResult<FamilyOutput>> CreateFamilyAsync(
        [FromBody] CreateFamilyInput createFamilyInput,
        [FromServices] IFamilyService familyService)
    {
        var familyOutput = await familyService.CreateFamilyAsync(createFamilyInput);
        return Created("", familyOutput);
    }
}