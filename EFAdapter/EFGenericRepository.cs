
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq;
using System.Linq.Expressions;

namespace EFAdapter
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly IUOW uow;

        private readonly DbSet<T> dbSet;

        private DbContext context
        {
            get
            {
                return (DbContext)uow.Context;
            }
        }

        public void Delete(object id)
        {
            T? entityToDelete = GetByID(id);
            if (entityToDelete != null)
            {
                dbSet.Remove(entityToDelete);                
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T? GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public EFRepository(IUOW uow)
        {
            this.uow = uow;
            dbSet = context.Set<T>();
        }

    }
}