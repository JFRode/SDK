using System;

namespace SDK.Base
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
        public bool Excluido { get; set; }
    }
}