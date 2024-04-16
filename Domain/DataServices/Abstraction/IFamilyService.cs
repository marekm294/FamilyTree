using Shared.Models.Inputs.Families;
using Shared.Models.Outputs;

namespace Domain.DataServices.Abstraction;

public interface IFamilyService
{
    Task<FamilyOutput> CreateFamilyAsync(
        CreateFamilyInput createFamilyInput,
        CancellationToken cancellationToken = default);
}