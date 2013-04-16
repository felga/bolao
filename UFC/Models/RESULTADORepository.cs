using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UFC;

namespace UFC.Models
{ 
    public class RESULTADORepository : IRESULTADORepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<RESULTADO> All
        {
            get { return context.RESULTADOes; }
        }

        public IQueryable<RESULTADO> AllIncluding(params Expression<Func<RESULTADO, object>>[] includeProperties)
        {
            IQueryable<RESULTADO> query = context.RESULTADOes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RESULTADO Find(int id)
        {
            return context.RESULTADOes.Find(id);
        }

        public void InsertOrUpdate(RESULTADO resultado)
        {
            if (resultado.ID == default(int)) {
                // New entity
                context.RESULTADOes.Add(resultado);
            } else {
                // Existing entity
                context.Entry(resultado).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var resultado = context.RESULTADOes.Find(id);
            context.RESULTADOes.Remove(resultado);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IRESULTADORepository
    {
        IQueryable<RESULTADO> All { get; }
        IQueryable<RESULTADO> AllIncluding(params Expression<Func<RESULTADO, object>>[] includeProperties);
        RESULTADO Find(int id);
        void InsertOrUpdate(RESULTADO resultado);
        void Delete(int id);
        void Save();
    }
}