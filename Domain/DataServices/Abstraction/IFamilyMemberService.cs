using Shared.Models.Outputs;

namespace Domain.DataServices.Abstraction;

public interface IFamilyMemberService
{
    Task<List<FamilyMemberOutput>> GetAllFamilyMemberOutputsAsync(CancellationToken cancellationToken = default);
}