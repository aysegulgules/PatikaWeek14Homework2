using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaWeek14Homework2.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable //Gerektiğinde garbage collector çalıştırır.
    {
        //Kaç kayda etki ettiğini geriye döner.
        Task<int> SaveChangesAsync();
        Task BeginTransaction();

        Task CommitTransaction();

        Task RollBackTransaction();
    }
}
