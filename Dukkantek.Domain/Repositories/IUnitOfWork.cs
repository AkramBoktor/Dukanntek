using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserManagerRepository UserManagerRepository { get; }
        IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
        IProductRepository ProductRepository { get; }

        int Complete();
    }
}