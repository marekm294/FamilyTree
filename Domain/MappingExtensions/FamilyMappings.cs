using Domain.Entities;
using Domain.Mappings;
using Shared.Models.Outputs;

namespace Domain.MappingExtensions;

internal static class FamilyMappings
{
    private static readonly FamilyMapping FAMILY_MAPPING = new FamilyMapping();

    public static FamilyOutput ToFamilyOutput(this IFamily family)
    {
        return FAMILY_MAPPING.IFamilyToFamilyOutput(family);
    }
}