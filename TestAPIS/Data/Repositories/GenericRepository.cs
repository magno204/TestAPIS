namespace TestAPIS.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext context;

        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public T GetById(long id)
        {
            return this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public T Create(T entity)
        {
            this.context.Set<T>().Add(entity);
            SaveAll();
            return entity;
        }

        public T CreateWithoutSave(T entity)
        {
            this.context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        public T Update(T entity)
        {
            this.context.Set<T>().Update(entity);
            SaveAll();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public void Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
            this.SaveAll();
        }

        public async Task<bool> ExistAsync(long id)
        {
            return await this.context.Set<T>().AnyAsync(e => e.Id == id);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public int SaveAll()
        {
            return this.context.SaveChanges();
        }
    }
}
