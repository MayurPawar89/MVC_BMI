using System;
using System.Collections.Generic;
using System.Data;
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

        #region "Properties"
        public Int64 CELEBINAS_ID { get; set; }
        public string AIRLN_CODE { get; set; }
        public string FLTNO { get; set; }

        public string FLIGHT_TYPE { get; set; }

        public DateTime DEP_DATE { get; set; }

        public string AIRCRFT_REG { get; set; }

        public string AIRCFT_TYPE { get; set; }

        public string AIRCFT_CODE { get; set; }

        public string BAYNO { get; set; }

        public string BME_NO { get; set; }

        public string BME_TYPE { get; set; }

        public DateTime BME_ST_DT { get; set; }

        public DateTime BME_END_DT { get; set; }

        public decimal CONSUMPTION { get; set; }

        public decimal BILLABLEDATA { get; set; }

        public string ENTRYTYPE { get; set; }

        public string ARRIVAL_FLIGHT_TYPE { get; set; }

        public string ARRIVAL_FLIGHT_NO { get; set; }
        public decimal MIN_USAGE { get; set; }
        public decimal ROUNDINNG_OFF_STATUS { get; set; }
        public decimal THRESHOLD_LIMIT { get; set; }
        public decimal FGP_RATE_PER_PER_HR { get; set; }
        public decimal PAC_RATE_PER_HR { get; set; }
        public decimal FGP_REVENUE { get; set; }
        public decimal PCA_REVENUE { get; set; }
        public decimal TOTAL { get; set; }

        #endregion

        #region "Methods"

        public List<AssociationData> GetAssociationData(DateTime dtFromDate, DateTime dtToDate, string sTask)
        {
            DataTable _dt = null;
            List<AssociationData> lstAssociationData = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@FROM_DATE_TIME", dtFromDate, ParameterDirection.Input, SqlDbType.DateTime);
                _DBParameters.Add("@TO_DATE_TIME", dtToDate, ParameterDirection.Input, SqlDbType.DateTime);
                //_DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("GET_ASSOCIATION_DATA", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstAssociationData = new List<AssociationData>();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        AssociationData oAssociationData = new AssociationData();
                        //ID	AIRLN CODE	A FLTNO	D FLTNO	A FLT	D FLT	D DATE	AIRCRFT REG	AIRCFT TYPE	AIRCFT CODE	BAYNO	
                        //BME NO	BME TYPE	BME ST DT	BME END DT	CONSUMPTION	BILLABLEDATA	FGP RATE	PAC RATE	
                        //FGP REVENUE	PCA REVENUE	TOTAL

                        oAssociationData.CELEBINAS_ID = Convert.ToInt64(dr["ID"]);
                        oAssociationData.AIRLN_CODE = Convert.ToString(dr["AIRLN CODE"]);
                        oAssociationData.ARRIVAL_FLIGHT_NO = Convert.ToString(dr["A FLTNO"]);
                        oAssociationData.FLTNO = Convert.ToString(dr["D FLTNO"]);
                        oAssociationData.ARRIVAL_FLIGHT_TYPE = Convert.ToString(dr["A FLT"]);
                        oAssociationData.FLIGHT_TYPE = Convert.ToString(dr["D FLT"]);
                        oAssociationData.DEP_DATE = Convert.ToDateTime(dr["D DATE"]);
                        oAssociationData.AIRCRFT_REG = Convert.ToString(dr["AIRCRFT REG"]);
                        oAssociationData.AIRCFT_TYPE = Convert.ToString(dr["AIRCFT TYPE"]);
                        oAssociationData.AIRCFT_CODE = Convert.ToString(dr["AIRCFT CODE"]);
                        oAssociationData.BAYNO = Convert.ToString(dr["BAYNO"]);
                        oAssociationData.BME_NO = Convert.ToString(dr["BME NO"]);
                        oAssociationData.BME_TYPE = Convert.ToString(dr["BME TYPE"]);
                        oAssociationData.BME_ST_DT = Convert.ToDateTime(dr["BME ST DT"]);
                        oAssociationData.BME_END_DT = Convert.ToDateTime(dr["BME END DT"]);
                        oAssociationData.CONSUMPTION = Convert.ToDecimal(dr["CONSUMPTION"]);
                        oAssociationData.BILLABLEDATA = Convert.ToDecimal(dr["BILLABLEDATA"]);
                        oAssociationData.FGP_RATE_PER_PER_HR = dr["FGP RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FGP RATE"]);
                        oAssociationData.PAC_RATE_PER_HR = dr["PAC RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PAC RATE"]);
                        oAssociationData.FGP_REVENUE = dr["FGP REVENUE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FGP REVENUE"]);
                        oAssociationData.PCA_REVENUE = dr["PCA REVENUE"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PCA REVENUE"]);
                        oAssociationData.TOTAL = dr["TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TOTAL"]);
                        lstAssociationData.Add(oAssociationData);
                        oAssociationData.Dispose();
                        oAssociationData = null;
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

            return lstAssociationData;
        }

        #endregion
    }
}