using Dukkantek.Data;
using Dukkantek.Domain.Repositories;

namespace Dukkantek.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IUserManagerRepository userManagerRepository,
            IUserRefreshTokenRepository userRefreshTokenRepository,
            IProductRepository productRepository)
        {
            _context = context;
            UserManagerRepository = userManagerRepository;
            ProductRepository = productRepository;
            UserRefreshTokenRepository = userRefreshTokenRepository;
           
        }

        public IUserManagerRepository UserManagerRepository { get; }
        public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
        public IProductRepository ProductRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
