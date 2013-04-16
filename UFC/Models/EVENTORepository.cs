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
    public class EVENTORepository : IEVENTORepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<EVENTO> All
        {
            get { return context.EVENTOes; }
        }

        public IQueryable<EVENTO> AllIncluding(params Expression<Func<EVENTO, object>>[] includeProperties)
        {
            IQueryable<EVENTO> query = context.EVENTOes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public EVENTO Find(int id)
        {
            return context.EVENTOes.Find(id);
        }

        public void InsertOrUpdate(EVENTO evento)
        {
            if (evento.ID == default(int)) {
                // New entity
                context.EVENTOes.Add(evento);
            } else {
                // Existing entity
                context.Entry(evento).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var evento = context.EVENTOes.Find(id);
            context.EVENTOes.Remove(evento);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IEVENTORepository
    {
        IQueryable<EVENTO> All { get; }
        IQueryable<EVENTO> AllIncluding(params Expression<Func<EVENTO, object>>[] includeProperties);
        EVENTO Find(int id);
        void InsertOrUpdate(EVENTO evento);
        void Delete(int id);
        void Save();
    }
}