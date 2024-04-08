using Domain.DataServices.Abstraction;
using Domain.DataServicesAbstraction;
using Domain.Entities;
using Domain.Extensions;
using Domain.MappingExtensions;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using Shared.QueryArgs;

namespace Domain.DataServices;

internal sealed class FamilyMemberService : IFamilyMemberService
{
    private readonly IQueryable<IFamilyMember> _familyMembers;
    private readonly IEntityProvider _entityProvider;
    private readonly IDbOperation _dbOperation;

    public FamilyMemberService(
        IQueryable<IFamilyMember> familyMembers,
        IEntityProvider entityProvider,
        IDbOperation dbOperation)
    {
        _familyMembers = familyMembers ?? throw new ArgumentNullException(nameof(familyMembers));
        _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
        _dbOperation = dbOperation ?? throw new ArgumentNullException(nameof(dbOperation));
    }
    public Task<List<FamilyMemberOutput>> GetAllFamilyMemberOutputsAsync(CancellationToken cancellationToken = default)
    {
        return _familyMembers
            .Select(fm => fm.ToFamilyMemberOutput())
            .ToListAsync(cancellationToken);
    }

    public async Task<FamilyMemberOutput> CreateFamilyMemberAsync(
        CreateFamilyMemberInput createFamilyMemberInput,
        CancellationToken cancellationToken = default)
    {
        var familyMemberEntity = _entityProvider.GetNewEntity<IFamilyMember>();
        familyMemberEntity.InitializeFamilyMember(createFamilyMemberInput);
        await _dbOperation.AddAsync(familyMemberEntity, cancellationToken);
        await _dbOperation.SaveChangesAsync(cancellationToken);

        return familyMemberEntity.ToFamilyMemberOutput();
    }

    public async Task<FamilyMemberOutput> UpdateFamilyMemberAsync(
        UpdateFamilyMemberInput updateFamilyMemberInput,
        CancellationToken cancellationToken = default)
    {
        var familyMemberEntity = _entityProvider.GetExistingEntity<IFamilyMember>(
            updateFamilyMemberInput.Id,
            updateFamilyMemberInput.Version);

        _dbOperation.AllowUpdate(familyMemberEntity);
        familyMemberEntity.UpdateFamilyMember(updateFamilyMemberInput);
        await _dbOperation.SaveChangesAsync(cancellationToken);
     
        return familyMemberEntity.ToFamilyMemberOutput();
    }

    public async Task<bool> DeleteFamilyMemberAsync(
        DeleteQueryArgs deleteQueryArgs,
        CancellationToken cancellationToken = default)
    {
        var familyMemberEntity = _entityProvider.GetExistingEntity<IFamilyMember>(
            deleteQueryArgs.Id,
            deleteQueryArgs.Version);

        _dbOperation.Remove(familyMemberEntity);
        await _dbOperation.SaveChangesAsync(cancellationToken);

        return true;
    }
}