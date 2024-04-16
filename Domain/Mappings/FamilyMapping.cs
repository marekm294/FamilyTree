using Domain.Entities;
using Riok.Mapperly.Abstractions;
using Shared.Models.Outputs;

namespace Domain.Mappings;

[Mapper]
public partial class FamilyMapping
{
    public partial FamilyOutput IFamilyToFamilyOutput(IFamily family);
}