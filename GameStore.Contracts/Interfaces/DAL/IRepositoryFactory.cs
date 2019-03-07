namespace GameStore.Contracts.Interfaces.DAL
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>() where T : class;
    }
}
