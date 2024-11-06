using PatikaWeek14Homework2.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly JwtDbContext _db;
        public IDbContextTransaction _dbTransaction;

        public UnitOfWork(JwtDbContext db)
        {
            _db= db;
        }

        public async Task BeginTransaction()
        {
           _dbTransaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _dbTransaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();

            //garbage collector çalışma izni verilen method
        }

        public async Task RollBackTransaction()
        {
            await _dbTransaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
