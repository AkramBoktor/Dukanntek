using System;
using System.Collections.Generic;
using Dukkantek.Domain.Interfaces.Mapper;
using System.Text;

namespace Dukkantek.Domain.Interfaces.Mapper
{
    public interface IMapperStore
    {
        IUserMapper UserMapper { get; }
        IProductMapper ProductMapper { get; }
        
    }
}
