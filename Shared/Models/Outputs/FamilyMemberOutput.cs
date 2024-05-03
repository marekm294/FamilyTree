using Shared.Models.Abstaction;
using Shared.Types;

namespace Shared.Models.Outputs;

public sealed class FamilyMemberOutput : IIdable
{
    public required Guid Id { get; init; }
    public required Guid FamilyId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string[] MiddleNames { get; init; }
    public required Event Birth { get; init; }
    public required Event Death { get; init; }
}