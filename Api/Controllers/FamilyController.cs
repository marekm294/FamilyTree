﻿using Api.Controllers.Abstraction;
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FamilyOutput>> CreateFamilyAsync(
        [FromBody] CreateFamilyInput createFamilyInput,
        [FromServices] IFamilyService familyService)
    {
        var familyOutput = await familyService.CreateFamilyAsync(createFamilyInput);
        return Created("", familyOutput);
    }

}