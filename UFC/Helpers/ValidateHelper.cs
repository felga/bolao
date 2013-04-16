using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UFC;
using System.Data.Entity.Infrastructure;

namespace UFC.Helpers
{
    public class ValidateHelper
    {
        public Boolean Duplicados(string campo, string valor, int id, string entidade)
        {
            int count = 0;
            UFC.Models.UFCEntities context = new UFC.Models.UFCEntities();
            switch (entidade)
            {
                case "USUARIO":
                    count = context.Usuario.SqlQuery("select * from " + entidade + " where " + campo + " = {0} and Id <> {1}", valor, id).Count();
                    break;
                //case "ES0EMPRT":
                //    count = context.ES0EMPRT.SqlQuery("select * from " + entidade + " where " + campo + " = {0} and Id <> {1}", valor, id).Count();
                //    break;
                //case "ES1PROJT":
                //    count = context.ES1PROJT.SqlQuery("select * from " + entidade + " where " + campo + " = {0} and Id <> {1}", valor, id).Count();
                //    break;
            }

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean Duplicados(string campo, string valor, int id, string entidade, string entidadeId)
        {
            int count = 0;
            UFC.Models.UFCEntities context = new UFC.Models.UFCEntities();
            switch (entidade)
            {
                case "USUARIO":
                    count = context.Usuario.SqlQuery("select * from " + entidade + " where ES0EMPRTId = {2} and " + campo + " = {0} and Id <> {1}", valor, id, entidadeId).Count();
                    break;
            }

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}