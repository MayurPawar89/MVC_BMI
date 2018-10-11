using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    public class User
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public User()
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

        ~User()
        {
            Dispose(false);
        }
        #endregion
        //  User_ID	Role_id	UserName	Password	LastName	FirstName	MiddelName	
        //MobileNo1	MobileNo2	RegDate	DOB	Gender	EmailID	Address	ActivationStatus	
        //Created_By_User_ID	Created_Date_Time
        #region "Properties"
        public Int64 User_ID { get; set; }
        public Int64 Role_ID { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Last Name")]
        
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Primary Mobile")]
        
        public string Mobile1 { get; set; }
        [Display(Name = "Secondry Mobile")]
        
        public string Mobile2 { get; set; }
        [Display(Name = "Registration Date")]
        
        public DateTime RegDate { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        [Display(Name = "Gender")]
        
        public string Gender { get; set; }
        [Display(Name = "Email ID")]
        
        public string EmailID { get; set; }
        [Display(Name = "Address")]
        
        public string Address { get; set; }
        [Display(Name = "Activation Status")]
        
        public string ActivationStatus { get; set; }
        public Int64 Created_By_User_ID { get; set; }
        public DateTime Created_DateTime { get; set; }

        [Display(Name="New Password")]
        public string NewPassword { get; set; }
        [Display(Name="Confirm Password")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        [Display(Name="Role Name")]
        public string Role_Name { get; set; }
        #endregion

        #region "Methods"
        public bool IsValidUser(string sUserName,string sPassword, out User oUser)
        {
            bool bIsValidUser = false;
            DataTable _dt = null;
            oUser = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@UserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@Password", sPassword, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_USER_LOGIN", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt!=null&&_dt.Rows.Count>0)
                {
                    if (Convert.ToInt64(_dt.Rows[0][0])!=-1)
                    {
                        bIsValidUser = true;
                        foreach (DataRow dr in _dt.Rows)
                        {
                            oUser = new User();
                            oUser.User_ID = Convert.ToInt64(dr["User_ID"]);
                            oUser.Role_ID = Convert.ToInt64(dr["Role_ID"]);
                            oUser.UserName = Convert.ToString(dr["UserName"]);
                            oUser.Role_Name = Convert.ToString(dr["Role_Name"]);
                            oUser.FirstName = Convert.ToString(dr["FirstName"]);
                            oUser.LastName = Convert.ToString(dr["LastName"]);
                        }
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

            return bIsValidUser;
        }

        public int InsertUpdateDeleteUser(User oUser, string sTask)
        {
            int nResult=-2;
            DataTable _dt = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@User_ID", oUser.User_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@Role_ID", oUser.Role_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@UserName", oUser.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@Password", oUser.Password, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@LastName", oUser.LastName, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@FirstName", oUser.FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@MiddelName", oUser.MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@MobileNo1", oUser.Mobile1, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@MobileNo2", oUser.Mobile2, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@DOB", oUser.DOB, ParameterDirection.Input, SqlDbType.Date);
                _DBParameters.Add("@Gender", oUser.Gender, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@EmailID", oUser.EmailID, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@Address", oUser.Address, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@ActivationStatus", oUser.ActivationStatus, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@Created_By_User_ID", oUser.Created_By_User_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _DataAccess.Retrive("SP_Create_Update_User", _DBParameters, out _dt);

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

        public List<User> GetUserInformation(Int64 nUserID=0)
        {
            DataTable _dt = null;
            List<User> lstUser = null;
            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                string sTask = string.Empty;
                if (nUserID==0)
                {
                    sTask = "ALL";
                }
                else
                {
                    sTask = "ONE";
                }
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@User_ID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@TASK", sTask, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_GET_USER_DATA", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    lstUser = new List<User>();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        User oUser = new User();
                        oUser.User_ID = Convert.ToInt64(dr["User_ID"]);
                        oUser.Role_ID = Convert.ToInt64(dr["Role_ID"]);
                        oUser.UserName = Convert.ToString(dr["UserName"]);
                        oUser.Password = Convert.ToString(dr["Password"]);
                        oUser.Role_Name = Convert.ToString(dr["Role_Name"]);
                        oUser.FirstName = Convert.ToString(dr["FirstName"]);
                        oUser.LastName = Convert.ToString(dr["LastName"]);
                        oUser.MiddleName = Convert.ToString(dr["MiddelName"]);
                        oUser.Mobile1 = Convert.ToString(dr["MobileNo1"]);
                        oUser.Mobile2 = Convert.ToString(dr["MobileNo2"]);
                        oUser.RegDate = Convert.ToDateTime(dr["RegDate"]);
                        oUser.DOB = Convert.ToDateTime(dr["DOB"]).Date;
                        oUser.Gender = Convert.ToString(dr["Gender"]);
                        oUser.EmailID = Convert.ToString(dr["EmailID"]);
                        oUser.Address = Convert.ToString(dr["Address"]);
                        oUser.ActivationStatus = Convert.ToString(dr["ActivationStatus"]);
                        oUser.Created_By_User_ID = Convert.ToInt64(dr["Created_By_User_ID"]);
                        oUser.Created_DateTime = Convert.ToDateTime(dr["Created_Date_Time"]);
                        lstUser.Add(oUser);
                        oUser.Dispose();
                        oUser = null;
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

            return lstUser;
        }
        public Int32 ChangePassword(Int64 nUserID,string sCurrentPassword,string sNewPassword)
        {
            Int32 nResult = 0;
            DataTable _dt = null;

            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@User_ID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@CurrentPassword", sCurrentPassword, ParameterDirection.Input, SqlDbType.VarChar);
                _DBParameters.Add("@NewPassword", sNewPassword, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_CHANGE_PASSWORD", _DBParameters, out _dt);

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
        public bool CheckValidPassword(Int64 nUserID, string sCurrentPassword)
        {
            bool bIsValidPassword = false;
            DataTable _dt = null;

            DBParameters _DBParameters = new DBParameters();
            DataAccess _DataAccess = new DataAccess();
            try
            {
                _DataAccess.OpenConnection(false);
                _DBParameters.clear();
                _DBParameters.Add("@User_ID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                _DBParameters.Add("@CurrentPassword", sCurrentPassword, ParameterDirection.Input, SqlDbType.VarChar);
                _DataAccess.Retrive("SP_CHECK_PASSWORD", _DBParameters, out _dt);

                _DataAccess.CloseConnection(false);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0][0]) == 1)
                    {
                        bIsValidPassword = true;
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

            return bIsValidPassword;
        }
        #endregion
    }
}