﻿namespace Shared.DDD;
public interface IAggregate<TId> : IAggregate, IEntity<TId> 
{
}

public interface IAggregate:IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}
