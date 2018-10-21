using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class AssociationData
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public AssociationData()
        {
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~AssociationData()
        {
            Dispose(false);
        }
        #endregion
    }
}