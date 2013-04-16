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
    public class LUTADORRepository : ILUTADORRepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<LUTADOR> All
        {
            get { return context.LUTADORs; }
        }

        public IQueryable<LUTADOR> AllIncluding(params Expression<Func<LUTADOR, object>>[] includeProperties)
        {
            IQueryable<LUTADOR> query = context.LUTADORs;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public LUTADOR Find(int id)
        {
            return context.LUTADORs.Find(id);
        }

        public void InsertOrUpdate(LUTADOR lutador)
        {
            if (lutador.ID == default(int)) {
                // New entity
                context.LUTADORs.Add(lutador);
            } else {
                // Existing entity
                context.Entry(lutador).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var lutador = context.LUTADORs.Find(id);
            context.LUTADORs.Remove(lutador);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface ILUTADORRepository
    {
        IQueryable<LUTADOR> All { get; }
        IQueryable<LUTADOR> AllIncluding(params Expression<Func<LUTADOR, object>>[] includeProperties);
        LUTADOR Find(int id);
        void InsertOrUpdate(LUTADOR lutador);
        void Delete(int id);
        void Save();
    }
}