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
    public class APOSTARepository : IAPOSTARepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<APOSTA> All
        {
            get { return context.APOSTAs; }
        }

        public IQueryable<APOSTA> AllIncluding(params Expression<Func<APOSTA, object>>[] includeProperties)
        {
            IQueryable<APOSTA> query = context.APOSTAs;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public APOSTA Find(int id)
        {
            return context.APOSTAs.Find(id);
        }

        public void InsertOrUpdate(APOSTA aposta)
        {
            if (aposta.ID == default(int)) {
                // New entity
                context.APOSTAs.Add(aposta);
            } else {
                // Existing entity
                context.Entry(aposta).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var aposta = context.APOSTAs.Find(id);
            context.APOSTAs.Remove(aposta);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IAPOSTARepository
    {
        IQueryable<APOSTA> All { get; }
        IQueryable<APOSTA> AllIncluding(params Expression<Func<APOSTA, object>>[] includeProperties);
        APOSTA Find(int id);
        void InsertOrUpdate(APOSTA aposta);
        void Delete(int id);
        void Save();
    }
}