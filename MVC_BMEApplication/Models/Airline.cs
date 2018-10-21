using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class Airline
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public Airline()
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

        ~Airline()
        {
            Dispose(false);
        }
        #endregion

        #region "Properties"
        //AIRLINE_CODE, AIRLINE_NAME, DOM_MIN_USAGE, INT_MIN_USAGE, DOM_ROUNDINNG_OFF_VALUE, INT_ROUNDINNG_OFF_VALUE, THRESHOLD_LIMIT, 
        //SAP_ACCOUNT_CODE, TAX_ACCOUNT_CODE, FGP_N_ARR_INT_DEP_INT_RATE, PCA_N_ARR_INT_DEP_INT_RATE, 
        //FGP_N_ARR_DOM_DEP_DOM_RATE, PAC_N_ARR_DOM_DEP_DOM_RATE, FGP_N_ARR_INT_DEP_DOM_RATE, PCA_N_ARR_INT_DEP_DOM_RATE, 
        //FGP_N_ARR_DOM_DEP_INT_RATE,PCA_N_ARR_DOM_DEP_INT_RATE,FGP_T_ARR_INT_DEP_INT_RATE,PCA_T_ARR_INT_DEP_INT_RATE,
        //FGP_T_ARR_DOM_DEP_DOM_RATE,PAC_T_ARR_DOM_DEP_DOM_RATE,FGP_T_ARR_INT_DEP_DOM_RATE,PCA_T_ARR_INT_DEP_DOM_RATE,
        //FGP_T_ARR_DOM_DEP_INT_RATE,PCA_T_ARR_DOM_DEP_INT_RATE
        [Display(Name = "Sr. No.")]
        public Int64 SRNO { get; set; }
        public Int64 AIRLINE_CODE_ID { get; set; }
        [Display(Name = "Airline Code")]
        public string AIRLINE_CODE { get; set; }
        [Display(Name = "Airline Name")]
        public string AIRLINE_NAME { get; set; }
        [Display(Name = "Domestic Min. Usage")]
        [StringLength(6)]
        public double DOM_MIN_USAGE { get; set; }
        [Display(Name = "International Min. Usage")]
        public double INT_MIN_USAGE { get; set; }
        [Display(Name = "Domestic Round Value")]
        public bool DOM_ROUNDINNG_OFF_VALUE { get; set; }
        [Display(Name = "International Round Value")]
        public bool INT_ROUNDINNG_OFF_VALUE { get; set; }
        [Display(Name = "Threshold Value")]
        public double THRESHOLD_LIMIT { get; set; }
        [Display(Name = "SAP Acc Code")]
        public string SAP_ACCOUNT_CODE { get; set; }
        [Display(Name = "Tax Acc Code")]
        public string TAX_ACCOUNT_CODE { get; set; }
      
      //,[FGP_N_ARR_INT_DEP_INT_RATE]
      //,[PCA_N_ARR_INT_DEP_INT_RATE]
      //,[FGP_N_ARR_DOM_DEP_DOM_RATE]
      //,[PAC_N_ARR_DOM_DEP_DOM_RATE]
      //,[FGP_N_ARR_INT_DEP_DOM_RATE]
      //,[PCA_N_ARR_INT_DEP_DOM_RATE]
      //,[FGP_N_ARR_DOM_DEP_INT_RATE]
      //,[PCA_N_ARR_DOM_DEP_INT_RATE]

        public double FGP_N_ARR_INT_DEP_INT_RATE { get; set; }
        public double PCA_N_ARR_INT_DEP_INT_RATE { get; set; }
        public double FGP_N_ARR_DOM_DEP_DOM_RATE { get; set; }
        public double PAC_N_ARR_DOM_DEP_DOM_RATE { get; set; }
        public double FGP_N_ARR_INT_DEP_DOM_RATE { get; set; }
        public double PCA_N_ARR_INT_DEP_DOM_RATE { get; set; }
        public double FGP_N_ARR_DOM_DEP_INT_RATE { get; set; }
        public double PCA_N_ARR_DOM_DEP_INT_RATE { get; set; }
      
      //,[FGP_T_ARR_INT_DEP_INT_RATE]
      //,[PCA_T_ARR_INT_DEP_INT_RATE]
      //,[FGP_T_ARR_DOM_DEP_DOM_RATE]
      //,[PAC_T_ARR_DOM_DEP_DOM_RATE]
      //,[FGP_T_ARR_INT_DEP_DOM_RATE]
      //,[PCA_T_ARR_INT_DEP_DOM_RATE]
      //,[FGP_T_ARR_DOM_DEP_INT_RATE]
      //,[PCA_T_ARR_DOM_DEP_INT_RATE]              
        public double FGP_T_ARR_INT_DEP_INT_RATE { get; set; }
        public double PCA_T_ARR_INT_DEP_INT_RATE { get; set; }
        public double FGP_T_ARR_DOM_DEP_DOM_RATE { get; set; }
        public double PAC_T_ARR_DOM_DEP_DOM_RATE { get; set; }
        public double FGP_T_ARR_INT_DEP_DOM_RATE { get; set; }
        public double PCA_T_ARR_INT_DEP_DOM_RATE { get; set; }
        public double FGP_T_ARR_DOM_DEP_INT_RATE { get; set; }
        public double PCA_T_ARR_DOM_DEP_INT_RATE { get; set; }
        #endregion

        #region "Methods"

        public int InsertUpdateDeleteAirline(Airline oAirline, string sTask)
        {
            int nResult = 0;
            DataTable _dt = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                //_DBParameters.Add("@Company_ID", oCompany.Company_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@AIRLINE_CODE_ID", oAirline.AIRLINE_CODE_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@AIRLINE_CODE", oAirline.AIRLINE_CODE, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@AIRLINE_NAME", oAirline.AIRLINE_NAME, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@DOM_MIN_USAGE", oAirline.DOM_MIN_USAGE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@INT_MIN_USAGE", oAirline.INT_MIN_USAGE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@DOM_ROUNDINNG_OFF_VALUE", oAirline.DOM_ROUNDINNG_OFF_VALUE, ParameterDirection.Input, SqlDbType.Bit);
                _DBParameters.Add("@INT_ROUNDINNG_OFF_VALUE", oAirline.INT_ROUNDINNG_OFF_VALUE, ParameterDirection.Input, SqlDbType.Bit);
                _DBParameters.Add("@THRESHOLD_LIMIT", oAirline.THRESHOLD_LIMIT, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@SAP_ACCOUNT_CODE", oAirline.SAP_ACCOUNT_CODE, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@TAX_ACCOUNT_CODE", oAirline.TAX_ACCOUNT_CODE, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@FGP_N_ARR_INT_DEP_INT_RATE", oAirline.FGP_N_ARR_INT_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_N_ARR_INT_DEP_INT_RATE", oAirline.PCA_N_ARR_INT_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_N_ARR_DOM_DEP_DOM_RATE", oAirline.FGP_N_ARR_DOM_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PAC_N_ARR_DOM_DEP_DOM_RATE", oAirline.PAC_N_ARR_DOM_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_N_ARR_INT_DEP_DOM_RATE", oAirline.FGP_N_ARR_INT_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_N_ARR_INT_DEP_DOM_RATE", oAirline.PCA_N_ARR_INT_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_N_ARR_DOM_DEP_INT_RATE", oAirline.FGP_N_ARR_DOM_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_N_ARR_DOM_DEP_INT_RATE", oAirline.PCA_N_ARR_DOM_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_INT_DEP_INT_RATE", oAirline.FGP_T_ARR_INT_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_T_ARR_INT_DEP_INT_RATE", oAirline.PCA_T_ARR_INT_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_DOM_DEP_DOM_RATE", oAirline.FGP_T_ARR_DOM_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PAC_T_ARR_DOM_DEP_DOM_RATE", oAirline.PAC_T_ARR_DOM_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_INT_DEP_DOM_RATE", oAirline.FGP_T_ARR_INT_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_T_ARR_INT_DEP_DOM_RATE", oAirline.PCA_T_ARR_INT_DEP_DOM_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_DOM_DEP_INT_RATE", oAirline.FGP_T_ARR_DOM_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_T_ARR_DOM_DEP_INT_RATE", oAirline.PCA_T_ARR_DOM_DEP_INT_RATE, ParameterDirection.Input, SqlDbType.Float);

                _DataAccess.Retrive("SP_Create_Update_Airline_Code_Master", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    nResult = Convert.ToInt32(_dt.Rows[0][0]);
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
            return nResult;
        }

        public List<Airline> GetAirlineInformation(Int64 nAIRLINE_CODE_ID, string sTask)
        {
            DataTable _dt = null;
            List<Airline> lstAirline = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@AIRLINE_CODE_ID", nAIRLINE_CODE_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_GET_AIRLINE_DATA", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstAirline = new List<Airline>();
                    Int64 count = 0;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        Airline oAirline = new Airline();
                        oAirline.SRNO = ++count;
                        oAirline.AIRLINE_CODE_ID = Convert.ToInt64(dr["AIRLINE_CODE_ID"]);
                        oAirline.AIRLINE_CODE = Convert.ToString(dr["AIRLINE_CODE"]);
                        oAirline.AIRLINE_NAME = Convert.ToString(dr["AIRLINE_NAME"]);
                        oAirline.DOM_MIN_USAGE = Convert.ToDouble(dr["DOM_MIN_USAGE"]);
                        oAirline.INT_MIN_USAGE = Convert.ToDouble(dr["INT_MIN_USAGE"]);
                        oAirline.DOM_ROUNDINNG_OFF_VALUE = Convert.ToBoolean(dr["DOM_ROUNDINNG_OFF_VALUE"]);
                        oAirline.INT_ROUNDINNG_OFF_VALUE = Convert.ToBoolean(dr["INT_ROUNDINNG_OFF_VALUE"]);
                        oAirline.THRESHOLD_LIMIT = Convert.ToDouble(dr["THRESHOLD_LIMIT"]);
                        oAirline.SAP_ACCOUNT_CODE = Convert.ToString(dr["SAP_ACCOUNT_CODE"]);
                        oAirline.TAX_ACCOUNT_CODE = Convert.ToString(dr["TAX_ACCOUNT_CODE"]);
                        oAirline.FGP_N_ARR_INT_DEP_INT_RATE = Convert.ToDouble(dr["FGP_N_ARR_INT_DEP_INT_RATE"]);
                        oAirline.PCA_N_ARR_INT_DEP_INT_RATE = Convert.ToDouble(dr["PCA_N_ARR_INT_DEP_INT_RATE"]);
                        oAirline.FGP_N_ARR_DOM_DEP_DOM_RATE = Convert.ToDouble(dr["FGP_N_ARR_DOM_DEP_DOM_RATE"]);
                        oAirline.PAC_N_ARR_DOM_DEP_DOM_RATE = Convert.ToDouble(dr["PAC_N_ARR_DOM_DEP_DOM_RATE"]);
                        oAirline.FGP_N_ARR_INT_DEP_DOM_RATE = Convert.ToDouble(dr["FGP_N_ARR_INT_DEP_DOM_RATE"]);
                        oAirline.PCA_N_ARR_INT_DEP_DOM_RATE = Convert.ToDouble(dr["PCA_N_ARR_INT_DEP_DOM_RATE"]);
                        oAirline.FGP_N_ARR_DOM_DEP_INT_RATE = Convert.ToDouble(dr["FGP_N_ARR_DOM_DEP_INT_RATE"]);
                        oAirline.PCA_N_ARR_DOM_DEP_INT_RATE = Convert.ToDouble(dr["PCA_N_ARR_DOM_DEP_INT_RATE"]);
                        oAirline.FGP_T_ARR_INT_DEP_INT_RATE = Convert.ToDouble(dr["FGP_T_ARR_INT_DEP_INT_RATE"]);
                        oAirline.PCA_T_ARR_INT_DEP_INT_RATE = Convert.ToDouble(dr["PCA_T_ARR_INT_DEP_INT_RATE"]);
                        oAirline.FGP_T_ARR_DOM_DEP_DOM_RATE = Convert.ToDouble(dr["FGP_T_ARR_DOM_DEP_DOM_RATE"]);
                        oAirline.PAC_T_ARR_DOM_DEP_DOM_RATE = Convert.ToDouble(dr["PAC_T_ARR_DOM_DEP_DOM_RATE"]);
                        oAirline.FGP_T_ARR_INT_DEP_DOM_RATE = Convert.ToDouble(dr["FGP_T_ARR_INT_DEP_DOM_RATE"]);
                        oAirline.PCA_T_ARR_INT_DEP_DOM_RATE = Convert.ToDouble(dr["PCA_T_ARR_INT_DEP_DOM_RATE"]);
                        oAirline.FGP_T_ARR_DOM_DEP_INT_RATE = Convert.ToDouble(dr["FGP_T_ARR_DOM_DEP_INT_RATE"]);
                        oAirline.PCA_T_ARR_DOM_DEP_INT_RATE = Convert.ToDouble(dr["PCA_T_ARR_DOM_DEP_INT_RATE"]);

                        lstAirline.Add(oAirline);
                        oAirline.Dispose();
                        oAirline = null;
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

            return lstAirline;
        }
        #endregion

        internal int DeleteAirline(long AIRLINE_CODE_ID, string sTask)
        {
            int nResult = 0;
            DataTable _dt = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                //_DBParameters.Add("@Company_ID", oCompany.Company_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@AIRLINE_CODE_ID",AIRLINE_CODE_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@AIRLINE_CODE", "", ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@AIRLINE_NAME", "", ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@DOM_MIN_USAGE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@INT_MIN_USAGE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@DOM_ROUNDINNG_OFF_VALUE", 0, ParameterDirection.Input, SqlDbType.Bit);
                _DBParameters.Add("@INT_ROUNDINNG_OFF_VALUE", 0, ParameterDirection.Input, SqlDbType.Bit);
                _DBParameters.Add("@THRESHOLD_LIMIT", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@SAP_ACCOUNT_CODE", 0, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@TAX_ACCOUNT_CODE", 0, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@FGP_N_ARR_INT_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_N_ARR_INT_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_N_ARR_DOM_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PAC_N_ARR_DOM_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_N_ARR_INT_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_N_ARR_INT_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_N_ARR_DOM_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_N_ARR_DOM_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_INT_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_T_ARR_INT_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_DOM_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PAC_T_ARR_DOM_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_INT_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_T_ARR_INT_DEP_DOM_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@FGP_T_ARR_DOM_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@PCA_T_ARR_DOM_DEP_INT_RATE", 0, ParameterDirection.Input, SqlDbType.Float);

                _DataAccess.Retrive("SP_Create_Update_Airline_Code_Master", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    nResult = Convert.ToInt32(_dt.Rows[0][0]);
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
            return nResult;
        }
    }
}