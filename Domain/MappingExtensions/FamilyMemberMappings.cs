using Domain.Entities;
using Domain.Mappings;
using Shared.Models.Outputs;

namespace Domain.MappingExtensions;

internal static class FamilyMemberMappings
{
    private static readonly FamilyMemberMapping FAMILY_MEMBER_MAPPING = new FamilyMemberMapping();

    public static FamilyMemberOutput ToFamilyMemberOutput(this IFamilyMember familyMember)
    {
        return FAMILY_MEMBER_MAPPING.IFamilyMemberToFamilyMemberOutput(familyMember);
    }
}