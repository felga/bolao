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
    public class PATROCINIORepository : IPATROCINIORepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<PATROCINIO> All
        {
            get { return context.PATROCINIOs; }
        }

        public IQueryable<PATROCINIO> AllIncluding(params Expression<Func<PATROCINIO, object>>[] includeProperties)
        {
            IQueryable<PATROCINIO> query = context.PATROCINIOs;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PATROCINIO Find(int id)
        {
            return context.PATROCINIOs.Find(id);
        }

        public void InsertOrUpdate(PATROCINIO patrocinio)
        {
            if (patrocinio.ID == default(int)) {
                // New entity
                context.PATROCINIOs.Add(patrocinio);
            } else {
                // Existing entity
                context.Entry(patrocinio).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var patrocinio = context.PATROCINIOs.Find(id);
            context.PATROCINIOs.Remove(patrocinio);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IPATROCINIORepository
    {
        IQueryable<PATROCINIO> All { get; }
        IQueryable<PATROCINIO> AllIncluding(params Expression<Func<PATROCINIO, object>>[] includeProperties);
        PATROCINIO Find(int id);
        void InsertOrUpdate(PATROCINIO patrocinio);
        void Delete(int id);
        void Save();
    }
}