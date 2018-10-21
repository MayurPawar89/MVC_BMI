using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class DataFetch
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public DataFetch()
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

        ~DataFetch()
        {
            Dispose(false);
        }
        #endregion
        #region "Properties"
        public Int64 SrNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Task { get; set; }
        public string CallingView { get; set; } 
        #endregion
        public List<BMEData> RetriveBMEData()
        {
            List<BMEData> lstBMEData = null;
            try
            {
                BMEData oBMEData =new BMEData();
                lstBMEData = oBMEData.GetBMEData(FromDate, ToDate, Task);
            }
            catch (Exception)
            {
            }
            return lstBMEData;
        }
        public List<StandData> RetriveStandData()
        {
            List<StandData> lstStandData = null;
            try
            {
                StandData oStandData = new StandData();
                lstStandData = oStandData.GetStandData(FromDate, ToDate, Task);
            }
            catch (Exception)
            {
            }
            return lstStandData;
        }
    }
}