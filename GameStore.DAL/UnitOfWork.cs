using GameStore.Contracts.Interfaces.DAL;
using GameStore.DAL.Context;

namespace GameStore.DAL
{
    public class UnitOfWork : IUnitOfWork
    {     
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly GameStoreContext _context;

        public UnitOfWork(IRepositoryFactory repositoryFactory, GameStoreContext context)
        { 
            _repositoryFactory = repositoryFactory;
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _repositoryFactory.CreateRepository<T>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
