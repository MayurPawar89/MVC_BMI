using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MVC_BMEApplication.Models
{
    public class Role
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public Role()
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

        ~Role()
        {
            Dispose(false);
        }
        #endregion
        #region "Properties"
        public Int64 RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleStatus { get; set; }
        #endregion

        #region "Method"
        public List<Role> GetAllRoles(Int64 nRoleID, string sTask)
        {
            DataTable _dt = null;
            List<Role> lstRole = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@Role_ID", nRoleID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_GET_USER_ROLE",_DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstRole = new List<Role>();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        Role oRole = new Role();
                        oRole.RoleID = Convert.ToInt64(dr["Role_ID"]);
                        oRole.RoleName = Convert.ToString(dr["Role_Name"]);
                        oRole.RoleStatus = Convert.ToString(dr["Satus"]);
                        lstRole.Add(oRole);
                        oRole.Dispose();
                        oRole = null;
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

            return lstRole;
        } 
        #endregion

    }
}