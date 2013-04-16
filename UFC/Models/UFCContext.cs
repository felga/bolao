using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UFC.Models
{
    public class UFCEntities : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<UFC.Models.UFCContext>());

        public DbSet<UFC.Usuario> Usuario { get; set; }

        public DbSet<UFC.LUTADOR> LUTADORs { get; set; }

        public DbSet<UFC.APOSTA> APOSTAs { get; set; }

        public DbSet<UFC.EVENTO> EVENTOes { get; set; }

        public DbSet<UFC.LUTA> LUTAs { get; set; }

        public DbSet<UFC.COMPROVANTE> COMPROVANTEs { get; set; }

        public DbSet<UFC.PATROCINIO> PATROCINIOs { get; set; }

        public DbSet<UFC.RESULTADO> RESULTADOes { get; set; }
    }
}