using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class BMEData
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public BMEData()
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

        ~BMEData()
        {
            Dispose(false);
        }
        #endregion

        #region "Properties"
        //ID,BME_ID,METER_TYPE,METER_SUB_TYPE,METER_LOCATION,METER_NUMBER,READING_DATE_TIME,CURRENT_READING,PREVIOUS_READING,
        //METER_START_TIME,METER_END_TIME,PROCESSING_DONE,PROCESSING_DATE_TIME,DATA_SOURCE_TYPE
        public Int64 SrNo { get; set; }
        public Int64 ID { get; set; }
        public string BME_ID { get; set; }
        public string METER_TYPE { get; set; }
        public string METER_SUB_TYPE { get; set; }
        public string METER_LOCATION { get; set; }
        public string METER_NUMBER { get; set; }
        public DateTime READING_DATE_TIME { get; set; }
        public string CURRENT_READING { get; set; }
        public string PREVIOUS_READING { get; set; }
        public DateTime METER_START_TIME { get; set; }
        public DateTime METER_END_TIME { get; set; }
        public bool PROCESSING_DONE { get; set; }
        public DateTime PROCESSING_DATE_TIME { get; set; }
        public string DATA_SOURCE_TYPE { get; set; }
        #endregion
        
        public List<BMEData> GetBMEData(DateTime dtFromDate, DateTime dtToDate, string sTask)
        {
            DataTable _dt = null;
            List<BMEData> lstBMEData = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@FROM_DATE_TIME", dtFromDate, ParameterDirection.Input, SqlDbType.DateTime);
                _DBParameters.Add("@TO_DATE_TIME", dtToDate, ParameterDirection.Input, SqlDbType.DateTime);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("GET_BME_DATA", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstBMEData = new List<BMEData>();
                    Int32 count = 0;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        BMEData oBMEData = new BMEData();
                        oBMEData.SrNo = ++count;
                        oBMEData.ID = Convert.ToInt64(dr["ID"]);
                        oBMEData.BME_ID = Convert.ToString(dr["BME ID"]);
                        oBMEData.METER_TYPE = Convert.ToString("");
                        oBMEData.METER_SUB_TYPE = Convert.ToString(dr["METER TYPE"]);
                        oBMEData.METER_LOCATION = Convert.ToString(dr["LOCATION"]);
                        oBMEData.METER_NUMBER = Convert.ToString(dr["METER NO."]);
                        oBMEData.READING_DATE_TIME = Convert.ToDateTime(dr["READING TIME"]);
                        oBMEData.CURRENT_READING = Convert.ToString("");
                        oBMEData.PREVIOUS_READING = Convert.ToString("");
                        oBMEData.METER_START_TIME = Convert.ToDateTime(dr["START TIME"]);
                        oBMEData.METER_END_TIME = Convert.ToDateTime(dr["END TIME"]);
                        if (Convert.ToString(dr["IS ASSOCIATED"]).ToLower() == "yes")
                        {
                            oBMEData.PROCESSING_DONE = true;
                        }
                        else
                        {
                            oBMEData.PROCESSING_DONE = false;
                        }
                        oBMEData.PROCESSING_DATE_TIME = Convert.ToDateTime(DateTime.MinValue);
                        oBMEData.DATA_SOURCE_TYPE = Convert.ToString(dr["DATA SOURCE"]);
                        lstBMEData.Add(oBMEData);
                        oBMEData.Dispose();
                        oBMEData = null;
                    }
                }
            }
            catch (Exception)
            {
                if (_DataAccess != null) { _DataAccess.RollBack(); _DataAccess.CloseConnection(false); }
            }
            finally
            {
                if (_DBParameters != null) { _DBParameters.Dispose(); }
            }

            return lstBMEData;
        }
    }
}