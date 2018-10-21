using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class StandData
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public StandData()
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

        ~StandData()
        {
            Dispose(false);
        }
        #endregion

        #region "Properties"
        public string FLIGHT_ID { get; set; }
        public string FLIGHT_NUMBER { get; set; }
        public DateTime FLIGHT_DATE { get; set; }
        public string AIRLINE_NAME { get; set; }
        public string AIRCRAFT_REGN { get; set; }
        public string AIRCRAFT_TYPE { get; set; }
        public string FLIGHT_TYPE { get; set; }
        public string STAND_NAME { get; set; }
        public DateTime STAND_IN_DATE { get; set; }
        public string STAND_IN_TIME { get; set; }
        public DateTime STAND_OUT_DATE { get; set; }
        public string STAND_OUT_TIME { get; set; }
        public bool PROCESSING_DONE { get; set; }
        public DateTime PROCESSING_DONE_TIMESTAMP { get; set; }
        public string ArrivalFlightType { get; set; }
        public string ArrivalFltNumber { get; set; }
        public string DATA_SOURCE_TYPE { get; set; }

        #endregion

        public List<StandData> GetStandData(DateTime dtFromDate, DateTime dtToDate, string sTask)
        {
            DataTable _dt = null;
            List<StandData> lstStandData = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@FROM_DATE_TIME", dtFromDate, ParameterDirection.Input, SqlDbType.DateTime);
                _DBParameters.Add("@TO_DATE_TIME", dtToDate, ParameterDirection.Input, SqlDbType.DateTime);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("GET_STAND_DATA", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstStandData = new List<StandData>();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        StandData oStandData = new StandData();
                        oStandData.FLIGHT_ID = Convert.ToString(dr["FLIGHT ID"]);
                        oStandData.FLIGHT_NUMBER = Convert.ToString(dr["FLIGHT No."]);
                        oStandData.FLIGHT_DATE = Convert.ToDateTime(dr["FLIGHT DATE"]);
                        oStandData.AIRLINE_NAME = Convert.ToString(dr["AIRLINE NAME"]);
                        oStandData.AIRCRAFT_REGN = Convert.ToString(dr["AIRCRAFT REGN"]);
                        oStandData.AIRCRAFT_TYPE = Convert.ToString(dr["AIRCRAFT TYPE"]);
                        oStandData.FLIGHT_TYPE = Convert.ToString(dr["FLIGHT TYPE"]);
                        oStandData.STAND_NAME = Convert.ToString(dr["STAND NAME"]);
                        oStandData.STAND_IN_DATE = Convert.ToDateTime(dr["STAND IN DATE"]);
                        oStandData.STAND_IN_TIME = Convert.ToString(dr["STAND IN TIME"]);
                        oStandData.STAND_OUT_DATE = Convert.ToDateTime(dr["STAND OUT DATE"]);
                        oStandData.STAND_OUT_TIME = Convert.ToString(dr["STAND OUT TIME"]);
                        if (Convert.ToString(dr["IS ASSOCIATED"]).ToLower() == "yes")
                        {
                            oStandData.PROCESSING_DONE = true;
                        }
                        else
                        {
                            oStandData.PROCESSING_DONE = false;
                        }
                        oStandData.DATA_SOURCE_TYPE = Convert.ToString(dr["DATA SOURCE"]);
                        lstStandData.Add(oStandData);
                        oStandData.Dispose();
                        oStandData = null;
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

            return lstStandData;
        }
    }
}