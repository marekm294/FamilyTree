using Domain.DataServices.Abstraction;
using Domain.DataServicesAbstraction;
using Domain.Entities;
using Domain.MappingExtensions;
using Shared.Models.Inputs.Families;
using Shared.Models.Outputs;

namespace Domain.DataServices;

internal sealed class FamilyService : IFamilyService
{
    private readonly IEntityProvider _entityProvider;
    private readonly IDbOperation _dbOperation;

    public FamilyService(
        IEntityProvider entityProvider,
        IDbOperation dbOperation)
    {
        _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
        _dbOperation = dbOperation ?? throw new ArgumentNullException(nameof(dbOperation));
    }

    public async Task<FamilyOutput> CreateFamilyAsync(
        CreateFamilyInput createFamilyInput,
        CancellationToken cancellationToken = default)
    {
        var familyEntity = _entityProvider.GetNewEntity<IFamily>();
        familyEntity.InitializeFamilyMember(createFamilyInput);
        await _dbOperation.AddAsync(familyEntity, cancellationToken);
        await _dbOperation.SaveChangesAsync(cancellationToken);

        return familyEntity.ToFamilyOutput();
    }

    public async Task<FamilyOutput> UpdateFamilyAsync(
        UpdateFamilyInput updateFamilyInput,
        CancellationToken cancellationToken = default)
    {
        var familyEntity = _entityProvider.GetExistingEntity<IFamily, UpdateFamilyInput>(updateFamilyInput);

        _dbOperation.AllowUpdate(familyEntity);
        familyEntity.UpdateFamily(updateFamilyInput);
        await _dbOperation.SaveChangesAsync(cancellationToken);

        return familyEntity.ToFamilyOutput();
    }
}