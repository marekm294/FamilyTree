using Domain.Entities.Abstraction;
using Shared.Types;

namespace Domain.Entities;

public interface IWedding : IEntity
{
    Guid? PartnerId1 { get; set; }
    Guid? PartnerId2 { get; set; }
    Event WeddingEvent { get; set; }
}