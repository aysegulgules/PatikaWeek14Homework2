using PatikaWeek14Homework2.Data.Context;
using PatikaWeek14Homework2.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public readonly JwtDbContext _db;
        public readonly DbSet<TEntity> _dbSet;

        public  Repository(JwtDbContext db)
        {
            _db = db;   
            _dbSet=db.Set<TEntity>();
            
        }

        public void Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _db.Add(entity);
        }

        public void Delete(TEntity entity, bool softDelete=true)
        {
            if(softDelete)
            { 
            entity.ModifiedDate = DateTime.Now;
            entity.IsDeleted = true;
            _db.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
           var entity= _dbSet.Find(id);
            Delete(entity);
        }
        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate is null ? _dbSet: _dbSet.Where(predicate);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

       
    }
}
