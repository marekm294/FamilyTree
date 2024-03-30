using Domain.DataServices.Abstraction;
using Domain.Entities;
using Domain.Extensions;
using Domain.MappingExtensions;
using Shared.Models.Outputs;

namespace Domain.DataServices;

internal sealed class FamilyMemberService : IFamilyMemberService
{
    private readonly IQueryable<IFamilyMember> _familyMembers;

    public FamilyMemberService(IQueryable<IFamilyMember> familyMembers)
    {
        _familyMembers = familyMembers ?? throw new ArgumentNullException(nameof(familyMembers));
    }

    public Task<List<FamilyMemberOutput>> GetAllFamilyMemberOutputsAsync(CancellationToken cancellationToken = default)
    {
        return _familyMembers
            .Select(fm => fm.ToFamilyMemberOutput())
            .ToListAsync(cancellationToken);
    }
}