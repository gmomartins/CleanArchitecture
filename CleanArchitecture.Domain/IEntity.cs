using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
