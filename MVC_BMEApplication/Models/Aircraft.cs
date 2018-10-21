using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class Aircraft
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public Aircraft()
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

        ~Aircraft()
        {
            Dispose(false);
        }
        #endregion

        #region "Properties"
        [Display(Name = "Sr. No.")]
        public Int64 SRNO { get; set; }
        [Display(Name = "Aircraft Type ID")]
        [Required]
        public Int64 AircraftTypeID { get; set; }
        [Display(Name = "Aircraft Type")]
        [Required]
        public string AircraftType { get; set; }
        [Display(Name = "Aircraft Code")]
        [Required]
        public string AircraftCode { get; set; } 
        #endregion

        #region "Methods"

        public int InsertUpdateDeleteAircraft(Aircraft oAircraft, string sTask)
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
                _DBParameters.Add("@AircraftType_ID", oAircraft.AircraftTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@AircraftType", oAircraft.AircraftType, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@AircraftCode", oAircraft.AircraftCode, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_Create_Update_Aircraft_Code_Master", _DBParameters, out _dt);

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

        public int DeleteAircraft(Int64 nAircraftTypeID, string sTask)
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
                _DBParameters.Add("@AircraftType_ID", nAircraftTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@AircraftType", "", ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@AircraftCode", "", ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_Create_Update_Aircraft_Code_Master", _DBParameters, out _dt);

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
        public List<Aircraft> GetAircraftInformation(Int64 nAircraftType_ID, string sTask)
        {
            DataTable _dt = null;
            List<Aircraft> lstAircraft = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@AircraftType_ID", nAircraftType_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_GET_AIRCRAFT_DATA", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstAircraft = new List<Aircraft>();
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        Aircraft oAircraft = new Aircraft();
                        oAircraft.SRNO = i+1;
                        oAircraft.AircraftTypeID = Convert.ToInt64(_dt.Rows[i]["AircraftType_ID"]);
                        oAircraft.AircraftType = Convert.ToString(_dt.Rows[i]["AircraftType"]);
                        oAircraft.AircraftCode = Convert.ToString(_dt.Rows[i]["AircraftCode"]);
                        lstAircraft.Add(oAircraft);
                        oAircraft.Dispose();
                        oAircraft = null;
                    }
                    //foreach (DataRow dr in _dt.Rows)
                    //{
                    //    Aircraft oAircraft = new Aircraft();
                    //    oAircraft.AircraftTypeID = Convert.ToInt64(dr["AircraftType_ID"]);
                    //    oAircraft.AircraftType = Convert.ToString(dr["AircraftType"]);
                    //    oAircraft.AircraftCode = Convert.ToString(dr["AircraftCode"]);
                    //    lstAircraft.Add(oAircraft);
                    //    oAircraft.Dispose();
                    //    oAircraft = null;
                    //}
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

            return lstAircraft;
        }
        #endregion
    }
}