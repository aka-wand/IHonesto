namespace IHonesto.Core.Data
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
