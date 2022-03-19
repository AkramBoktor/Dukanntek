
using Dukkantek.Domain.Interfaces.Mapper;

namespace Gezira.Service.Mapper
{
    public class MapperStore : IMapperStore
    {
        public MapperStore(
            IUserMapper userMapper,
            IProductMapper productMapper
          )
        {
            UserMapper = userMapper;
            ProductMapper = productMapper;
        }

        public IUserMapper UserMapper { get; }
        public IProductMapper ProductMapper { get; }
        

    }
}
