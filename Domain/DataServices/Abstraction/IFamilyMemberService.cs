﻿using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using Shared.QueryArgs;

namespace Domain.DataServices.Abstraction;

public interface IFamilyMemberService : IDataService
{
    Task<FamilyMemberOutput> CreateFamilyMemberAsync(
        CreateFamilyMemberInput createFamilyMemberInput,
        CancellationToken cancellationToken = default);
    Task<FamilyMemberOutput> UpdateFamilyMemberAsync(
        UpdateFamilyMemberInput updateFamilyMemberInput,
        CancellationToken cancellationToken = default);
    Task<bool> DeleteFamilyMemberAsync(
        DeleteQueryArgs deleteQueryArgs,
        CancellationToken cancellationToken = default);
    Task<List<FamilyMemberOutput>> GetFamilyMembersAsync(
        Guid familyId,
        CancellationToken cancellationToken = default);
}