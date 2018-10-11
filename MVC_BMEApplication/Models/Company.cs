using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class Company
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public Company()
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

        ~Company()
        {
            Dispose(false);
        }
        #endregion
        //[Company_ID],[Company_Name],[Billing_Address],[GST_No],[PAN_No],[HSN_Code],[GST_Per],[Contact_No],[EMail_ID]

        #region "Properties"
        public Int64 Company_ID { get; set; }
        public string Company_Name { get; set; }
        public string Billing_Address { get; set; }
        public string GST_No { get; set; }
        public string PAN_No { get; set; }
        public string HSN_Code { get; set; }
        public double GST_Per { get; set; }
        public string Contact_No { get; set; }
        public string Email_ID { get; set; } 
        #endregion

        #region "Methods"

        public int InsertUpdateCompnay(Company oCompany, string sTask)
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
                _DBParameters.Add("@Company_Name", oCompany.Company_Name, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@Billing_Address", oCompany.Billing_Address, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@GST_No", oCompany.GST_No, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@PAN_No", oCompany.PAN_No, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@HSN_Code", oCompany.HSN_Code, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@GST_Per", oCompany.GST_Per, ParameterDirection.Input, SqlDbType.Float);
                _DBParameters.Add("@Contact_No", oCompany.Contact_No, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@Email_ID", oCompany.Email_ID, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP__Update_Company_Master", _DBParameters, out _dt);

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

        public List<Company> GetCompanyInformation()
        {
            DataTable _dt = null;
            List<Company> lstCompany = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                string sTask = "ONE";
                
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@Company_ID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_Get_Company_Master_Data", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstCompany = new List<Company>();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        Company oCompany = new Company();
                        oCompany.Company_ID = Convert.ToInt64(dr["Company_ID"]);
                        oCompany.Company_Name = Convert.ToString(dr["Company_Name"]);
                        oCompany.Billing_Address = Convert.ToString(dr["Billing_Address"]);
                        oCompany.GST_No = Convert.ToString(dr["GST_No"]);
                        oCompany.PAN_No = Convert.ToString(dr["PAN_No"]);
                        oCompany.HSN_Code = Convert.ToString(dr["HSN_Code"]);
                        oCompany.GST_Per = Convert.ToDouble(dr["GST_Per"]);
                        oCompany.Contact_No = Convert.ToString(dr["Contact_No"]);
                        oCompany.Email_ID = Convert.ToString(dr["Email_ID"]);
                        lstCompany.Add(oCompany);
                        oCompany.Dispose();
                        oCompany = null;
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

            return lstCompany;
        } 
        #endregion
    }
}