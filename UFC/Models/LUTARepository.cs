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
    public class LUTARepository : ILUTARepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<LUTA> All
        {
            get { return context.LUTAs; }
        }

        public IQueryable<LUTA> AllIncluding(params Expression<Func<LUTA, object>>[] includeProperties)
        {
            IQueryable<LUTA> query = context.LUTAs;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public LUTA Find(int id)
        {
            return context.LUTAs.Find(id);
        }

        public void InsertOrUpdate(LUTA luta)
        {
            if (luta.ID == default(int)) {
                // New entity
                context.LUTAs.Add(luta);
            } else {
                // Existing entity
                context.Entry(luta).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var luta = context.LUTAs.Find(id);
            context.LUTAs.Remove(luta);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface ILUTARepository
    {
        IQueryable<LUTA> All { get; }
        IQueryable<LUTA> AllIncluding(params Expression<Func<LUTA, object>>[] includeProperties);
        LUTA Find(int id);
        void InsertOrUpdate(LUTA luta);
        void Delete(int id);
        void Save();
    }
}