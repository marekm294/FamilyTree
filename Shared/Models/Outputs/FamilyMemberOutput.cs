using Shared.Models.Abstaction;

namespace Shared.Models.Outputs;

public sealed class FamilyMemberOutput : IIdable
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime? BirthDate { get; init; }
    public required DateTime? DeathDate { get; init; }
}