using Shared.Models.Inputs.Families;
using Shared.Models.Outputs;

namespace Domain.DataServices.Abstraction;

public interface IFamilyService : IDataService
{
    Task<FamilyOutput> CreateFamilyAsync(
        CreateFamilyInput createFamilyInput,
        CancellationToken cancellationToken = default);

    Task<FamilyOutput> UpdateFamilyAsync(
        UpdateFamilyInput updateFamilyInput,
        CancellationToken cancellationToken = default);
}