using BaseCore.Infrastructure.Data;
using BaseCore.Utility.HelperModel;
using BeCore.Core.Base;
using BeCore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using BeCore.Core.Model;

namespace BaseCore.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase, new()
    {

        protected readonly BaseContext Context;

        public EfRepository(BaseContext context)
        {
            Context = context;
        }

        public T Add(T entity, bool IsCommit = false)
        {
            Context.Set<T>().Add(entity);
            if (IsCommit)
                Commit();
            return entity;
        }

        public void AddRange(IEnumerable<T> entities, bool IsCommit = false)
        {
            Context.Set<T>().AddRange(entities);
            if (IsCommit)
                Commit();

        }

        public void Attach(T entity)
        {
            Context.Set<T>().Attach(entity);
        }

        public void AttachAsModified(T entity)
        {
            Attach(entity);
            Update(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Attach(entity);
            }
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await Context.Set<T>().CountAsync(criteria);
        }

        public void Delete(T entity, bool IsCommit = false)
        {
            Context.Set<T>().Remove(entity);
            if (IsCommit)
                Commit();
        }

        public void DeleteRange(IEnumerable<T> entities, bool IsCommit = false)
        {
            foreach (var entity in entities)
            {
                Context.Set<T>().Remove(entity);
            }
            if (IsCommit)
                Commit();
        }

        public void DeleteWhere(Expression<Func<T, bool>> criteria, bool IsCommit = false)
        {
            IEnumerable<T> entities = Context.Set<T>().Where(criteria);
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Deleted;
            }
            if (IsCommit)
                Commit();
        }

        public void Detach(T entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public void DetachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(Context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            return await queryableResultWithIncludes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> criteria)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(criteria);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(Context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            return await queryableResultWithIncludes.FirstOrDefaultAsync(criteria);
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> criteria)
        {
            return Context.Set<T>().Where(criteria).AsEnumerable();
        }


        public int BatchUpdate(Expression<Func<T, bool>> where, T model, List<string> Columns = null)
        {
            //EFCore.BulkExtensions
            return Context.Set<T>().Where(where).BatchUpdate(model, Columns);
        }

        public int BatchDelete(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).BatchDelete();
        }

        public void ExecuteSql(string sql, object sqlPara)
        {
            Context.Database.ExecuteSqlCommand(sql, sqlPara);
        }

        public IEnumerable<T> List(IEnumerable<UosoConditions> conditions)
        {
            var parser = new UosoExpressionParser<T>();
            var filter = parser.ParserConditions(conditions);
            return List(filter);
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
         .Aggregate(Context.Set<T>().AsQueryable(),
             (current, include) => current.Include(include));
            return queryableResultWithIncludes.Where(criteria).AsEnumerable();
        }

        public IEnumerable<T> ListAll()
        {

            return Context.Set<T>().AsEnumerable();
        }

        //public IEnumerable<T> Upddate()
        //{

        //    return Context.Set<T>().AsEnumerable();
        //}

        public async Task<List<T>> ListAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria)
        {
            return await Context.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(Context.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            return await queryableResultWithIncludes.Where(criteria).ToListAsync();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public bool Commit()
        {
            return Context.SaveChanges() >= 0;
        }



        public bool Commit(bool acceptAllChangesOnSuccess)
        {
            return Context.SaveChanges(acceptAllChangesOnSuccess) >= 0;
        }

        public async Task<bool> CommitAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken) >= 0;
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken) >= 0;
        }

        public List<T> GetPage(Expression<Func<T, bool>> criteria, int pageindex, int pagesize, out int itemCount, string Field, string orderby)
        {
            //throw new NotImplementedException();
            return Context.Set<T>().Where(criteria).Pager(pageindex, pagesize, out itemCount).OrderBy(Field, orderby).ToList();
        }

        public LayGridData GetPaper(SearchModel model)
        {
            var parser = new UosoExpressionParser<T>();
            var filter = parser.ParserConditions(model.WhereLambda);
            var itemCount = 0;
            List<T> ls = new List<T>();
            if (model.WhereLambda.Count > 0)
                ls = Context.Set<T>().Where(filter).OrderBy(model.field, model.order).Pager(model.page, model.limit, out itemCount).ToList();
            else
                ls = Context.Set<T>().OrderBy(model.field, model.order).Pager(model.page, model.limit, out itemCount).ToList();

            return new LayGridData()
            {
                code = 0,
                count = (itemCount % model.limit == 0) ? itemCount / model.limit : itemCount / model.limit + 1,
                data = ls
            };
        }

        public LayGridData GetPaper(SearchModel model, Expression<Func<T, bool>> criteria)
        {
            var parser = new UosoExpressionParser<T>();
            var filter = parser.ParserConditions(model.WhereLambda);
            filter = filter.And(criteria);
            //filter.
            var itemCount = 0;
            List<T> ls = new List<T>();
            if (model.WhereLambda.Count > 0)
                ls = Context.Set<T>().Where(filter).OrderBy(model.field, model.order).Pager(model.page, model.limit, out itemCount).ToList();
            else if (model.WhereLambda.Count == 0 && criteria != null)
                ls = Context.Set<T>().Where(criteria).OrderBy(model.field, model.order).Pager(model.page, model.limit, out itemCount).ToList();

            else
                ls = Context.Set<T>().OrderBy(model.field, model.order).Pager(model.page, model.limit, out itemCount).ToList();

            return new LayGridData()
            {
                code = 0,
                count = (itemCount % model.limit == 0) ? itemCount / model.limit : itemCount / model.limit + 1,
                data = ls
            };
        }



    }
}
