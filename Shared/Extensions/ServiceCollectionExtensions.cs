﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models.Inputs.FamilyMembers;
using Shared.QueryArgs;
using Shared.Validators.FamilyMembers;
using Shared.Validators.QueryArgs;

namespace Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        return services
            .AddValidators();
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services
            .AddScoped<IValidator<CreateFamilyMemberInput>, CreateFamilyMemberInputValidator>()
            .AddScoped<IValidator<UpdateFamilyMemberInput>, UpdateFamilyMemberInputValidator>()
            
            .AddScoped<IValidator<DeleteQueryArgs>, DeleteQueryArgsValidator>();
    }
}