namespace TestAPIS.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(long id);

        T GetById(long id);

        Task<T> CreateAsync(T entity);

        T Create(T entity);

        T CreateWithoutSave(T entity);

        Task<T> UpdateAsync(T entity);

        T Update(T entity);

        Task DeleteAsync(T entity);

        void Delete(T entity);

        Task<bool> ExistAsync(long id);

        int SaveAll();
    }
}
