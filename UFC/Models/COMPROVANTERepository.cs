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
    public class COMPROVANTERepository : ICOMPROVANTERepository
    {
        UFCEntities context = new UFCEntities();

        public IQueryable<COMPROVANTE> All
        {
            get { return context.COMPROVANTEs; }
        }

        public IQueryable<COMPROVANTE> AllIncluding(params Expression<Func<COMPROVANTE, object>>[] includeProperties)
        {
            IQueryable<COMPROVANTE> query = context.COMPROVANTEs;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public COMPROVANTE Find(int id)
        {
            return context.COMPROVANTEs.Find(id);
        }

        public void InsertOrUpdate(COMPROVANTE comprovante)
        {
            if (comprovante.ID == default(int)) {
                // New entity
                context.COMPROVANTEs.Add(comprovante);
            } else {
                // Existing entity
                context.Entry(comprovante).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var comprovante = context.COMPROVANTEs.Find(id);
            context.COMPROVANTEs.Remove(comprovante);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface ICOMPROVANTERepository
    {
        IQueryable<COMPROVANTE> All { get; }
        IQueryable<COMPROVANTE> AllIncluding(params Expression<Func<COMPROVANTE, object>>[] includeProperties);
        COMPROVANTE Find(int id);
        void InsertOrUpdate(COMPROVANTE comprovante);
        void Delete(int id);
        void Save();
    }
}