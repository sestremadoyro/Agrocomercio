using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;

namespace pryAgrocomercioBLL.EntityCollection.Interfaces
{
    public interface IAbstractBase<T>
    {
        void Add(T pEntity);
        void Delete(T pEntity);
        void Attach(T pEntity);
        void Detach(T pEntity);
        void Update(T pEntity);
        T FindBy(Expression<Func<T, bool>> expression);
        IQueryable<T> Find(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        void SaveChanges();
        DataTable ToDataTable<T>(IQueryable<T> varlist);
    }
}
