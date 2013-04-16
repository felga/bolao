//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace UFC
{
    public partial class UFCEntities : ObjectContext
    {
        public const string ConnectionString = "name=UFCEntities";
        public const string ContainerName = "UFCEntities";
    
        #region Constructors
    
        public UFCEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public UFCEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public UFCEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<APOSTA> APOSTA
        {
            get { return _aPOSTA  ?? (_aPOSTA = CreateObjectSet<APOSTA>("APOSTA")); }
        }
        private ObjectSet<APOSTA> _aPOSTA;
    
        public ObjectSet<COMPROVANTE> COMPROVANTE
        {
            get { return _cOMPROVANTE  ?? (_cOMPROVANTE = CreateObjectSet<COMPROVANTE>("COMPROVANTE")); }
        }
        private ObjectSet<COMPROVANTE> _cOMPROVANTE;
    
        public ObjectSet<EVENTO> EVENTO
        {
            get { return _eVENTO  ?? (_eVENTO = CreateObjectSet<EVENTO>("EVENTO")); }
        }
        private ObjectSet<EVENTO> _eVENTO;
    
        public ObjectSet<LUTA> LUTA
        {
            get { return _lUTA  ?? (_lUTA = CreateObjectSet<LUTA>("LUTA")); }
        }
        private ObjectSet<LUTA> _lUTA;
    
        public ObjectSet<LUTADOR> LUTADOR
        {
            get { return _lUTADOR  ?? (_lUTADOR = CreateObjectSet<LUTADOR>("LUTADOR")); }
        }
        private ObjectSet<LUTADOR> _lUTADOR;
    
        public ObjectSet<PATROCINIO> PATROCINIO
        {
            get { return _pATROCINIO  ?? (_pATROCINIO = CreateObjectSet<PATROCINIO>("PATROCINIO")); }
        }
        private ObjectSet<PATROCINIO> _pATROCINIO;
    
        public ObjectSet<RESULTADO> RESULTADO
        {
            get { return _rESULTADO  ?? (_rESULTADO = CreateObjectSet<RESULTADO>("RESULTADO")); }
        }
        private ObjectSet<RESULTADO> _rESULTADO;
    
        public ObjectSet<Usuario> Usuario
        {
            get { return _usuario  ?? (_usuario = CreateObjectSet<Usuario>("Usuario")); }
        }
        private ObjectSet<Usuario> _usuario;

        #endregion
    }
}