//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace UFC
{
    public partial class LUTADOR
    {
        #region Primitive Properties
    
        public virtual int ID
        {
            get;
            set;
        }
    
        public virtual string NOME
        {
            get;
            set;
        }
    
        public virtual string PAGINA
        {
            get;
            set;
        }
    
        public virtual string CAMINHOFOTO
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<LUTA> LUTA
        {
            get
            {
                if (_lUTA == null)
                {
                    var newCollection = new FixupCollection<LUTA>();
                    newCollection.CollectionChanged += FixupLUTA;
                    _lUTA = newCollection;
                }
                return _lUTA;
            }
            set
            {
                if (!ReferenceEquals(_lUTA, value))
                {
                    var previousValue = _lUTA as FixupCollection<LUTA>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupLUTA;
                    }
                    _lUTA = value;
                    var newValue = value as FixupCollection<LUTA>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupLUTA;
                    }
                }
            }
        }
        private ICollection<LUTA> _lUTA;
    
        public virtual ICollection<LUTA> LUTA1
        {
            get
            {
                if (_lUTA1 == null)
                {
                    var newCollection = new FixupCollection<LUTA>();
                    newCollection.CollectionChanged += FixupLUTA1;
                    _lUTA1 = newCollection;
                }
                return _lUTA1;
            }
            set
            {
                if (!ReferenceEquals(_lUTA1, value))
                {
                    var previousValue = _lUTA1 as FixupCollection<LUTA>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupLUTA1;
                    }
                    _lUTA1 = value;
                    var newValue = value as FixupCollection<LUTA>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupLUTA1;
                    }
                }
            }
        }
        private ICollection<LUTA> _lUTA1;

        #endregion
        #region Association Fixup
    
        private void FixupLUTA(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LUTA item in e.NewItems)
                {
                    item.LUTADOR = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (LUTA item in e.OldItems)
                {
                    if (ReferenceEquals(item.LUTADOR, this))
                    {
                        item.LUTADOR = null;
                    }
                }
            }
        }
    
        private void FixupLUTA1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LUTA item in e.NewItems)
                {
                    item.LUTADOR1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (LUTA item in e.OldItems)
                {
                    if (ReferenceEquals(item.LUTADOR1, this))
                    {
                        item.LUTADOR1 = null;
                    }
                }
            }
        }

        #endregion
    }
}
