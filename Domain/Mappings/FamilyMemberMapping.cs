using Domain.Entities;
using Riok.Mapperly.Abstractions;
using Shared.Models.Outputs;

namespace Domain.Mappings;

[Mapper]
public partial class FamilyMemberMapping
{
    public partial FamilyMemberOutput IFamilyMemberToFamilyMemberOutput(IFamilyMember familyMember);
}