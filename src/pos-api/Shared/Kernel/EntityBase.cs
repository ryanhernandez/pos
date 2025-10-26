using System;

namespace Server.Shared.Kernel
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    }
}
