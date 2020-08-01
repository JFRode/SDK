using System;

namespace SDK.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Excluido { get; set; }
    }
}