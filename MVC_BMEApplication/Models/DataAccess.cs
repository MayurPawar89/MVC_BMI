using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_BMEApplication.Models
{
    class DataAccess : IDisposable
    {
        #region "Constructor & Distructor"
        private bool disposed = false;
        public DataAccess()
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

        ~DataAccess()
        {
            Dispose(false);
        }
        #endregion

        SqlConnection _con;
        Boolean _WithTrn = false;
        SqlTransaction _Trn;

        #region "Connection"
        public void OpenConnection(bool WithTrn)
        {
            string _conStr = "";
            _conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            _con = new SqlConnection(_conStr);
            _con.Open();

            _WithTrn = WithTrn;
            if (WithTrn)
            {
                _Trn = _con.BeginTransaction();
            }
        }

        public void RollBack()
        {
            if (_Trn != null && _WithTrn == true)
            {
                if (_Trn.Connection != null)
                {
                    _Trn.Rollback();
                }
            }
        }

        public void Commit()
        {
            if (_Trn != null && _WithTrn == true)
            {
                _Trn.Commit();
            }
        }

        public void CloseConnection(bool WithTrn)
        {
            if (_con != null)
            {
                if (WithTrn == true)
                {
                    if (_Trn != null)
                    {
                        _Trn.Commit();
                    }
                }
                if (_con.State != ConnectionState.Closed)
                {
                    _con.Close();
                }
            }
        }
        #endregion

        #region "Insert Update Delete - Stored Procedure"
        public int Execute(string StoredProcedureName, DBParameters Parameters)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            return _result;
        }

        public int Execute(string StoredProcedureName)
        {
            int _result = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            int _outputCounter = 0;
            SqlParameter osqlParameter;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].ParameterDirection == ParameterDirection.Output || Parameters[_counter].ParameterDirection == ParameterDirection.InputOutput)
                    {
                        _outputCounter = _counter;
                    }
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }


                _result = _sqlcommand.ExecuteNonQuery();

                if (_sqlcommand.Parameters[_outputCounter].Value != null)
                {
                    ParameterValue = _sqlcommand.Parameters[_outputCounter].Value;
                }
                else
                {
                    ParameterValue = 0;
                }

            }
            catch (Exception ex)
            {

                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue1, out object ParameterValue2)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            int _outputCounter1 = -1;
            int _outputCounter2 = -1;
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].ParameterDirection == ParameterDirection.Output || Parameters[_counter].ParameterDirection == ParameterDirection.InputOutput)
                    {
                        if (_outputCounter1 < 0) { _outputCounter1 = _counter; }
                        else if (_outputCounter2 < 0) { _outputCounter2 = _counter; }
                    }
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }


                _result = _sqlcommand.ExecuteNonQuery();

                if (_sqlcommand.Parameters[_outputCounter1].Value != null)
                { ParameterValue1 = _sqlcommand.Parameters[_outputCounter1].Value; }
                else { ParameterValue1 = 0; }

                if (_sqlcommand.Parameters[_outputCounter2].Value != null)
                { ParameterValue2 = _sqlcommand.Parameters[_outputCounter2].Value; }
                else
                { ParameterValue2 = 0; }

            }
            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName, DBParameters Parameters)
        {
            object _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }

            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }

            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }

            return _result;
        }

        #endregion

        #region "Insert Update Delete - SQL Query"

        public int Execute_Query(string SQLQuery)
        {
            int _result = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                _result = _sqlcommand.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            return _result;
        }

        public object ExecuteScalar_Query(string SQLQuery)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }

            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                throw ex;
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }

            return _result;
        }

        public object ExecuteScalar_Query(string SQLQuery, out string ErrorMessage)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();
            ErrorMessage = "";
            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = _con;

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (Exception ex)
            {
                if (_WithTrn == true)
                {
                    _Trn.Rollback();
                }
                ErrorMessage = ex.ToString();
                //throw ex;
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }

            return _result;
        }

        #endregion

        #region "Retrive Data - Stored Procedure"

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader _result)
        {
            //SqlDataReader _result;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }

                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

                _result = _sqlcommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet _result)
        {
            //DataSet _result = new DataSet();
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();

                //_dataAdapter.Fill(_result);
                _dataAdapter.Fill(_resultinternal);
                _dataAdapter.Dispose();

                _result = _resultinternal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
        {
            //DataTable _result = new DataTable();
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    _resultinternal = _dataset.Tables[0];
                }
                _result = _resultinternal;

                _resultinternal.Dispose();
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }


        // Without Parameters //
        public void Retrive(string StoredProcedureName, out SqlDataReader _result)
        {
            //SqlDataReader _result;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                _result = _sqlcommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, out DataSet _result)
        {
            //DataSet _result = new DataSet();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();

                //_dataAdapter.Fill(_result);
                _dataAdapter.Fill(_resultinternal);
                _dataAdapter.Dispose();

                _result = _resultinternal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, out DataTable _result)
        {
            //DataTable _result = new DataTable();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    _resultinternal = _dataset.Tables[0];
                }
                _result = _resultinternal;

                _resultinternal.Dispose();
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        #endregion

        #region "Retrive Data - SQL Query"

        public void Retrive_Query(string SQLQuery, out SqlDataReader _result)
        {
            //SqlDataReader _result;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                _result = _sqlcommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive_Query(string SQLQuery, out DataSet _result)
        {
            //DataSet _result = new DataSet();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = _con;
                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();

                //_dataAdapter.Fill(_result);
                _dataAdapter.Fill(_resultinternal);
                _dataAdapter.Dispose();

                _result = _resultinternal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive_Query(string SQLQuery, out DataTable _result)
        {
            //DataTable _result = new DataTable();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = _con;

                if (_WithTrn == true)
                {
                    _sqlcommand.Transaction = _Trn;
                }
                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    _resultinternal = _dataset.Tables[0];
                }
                _result = _resultinternal;

                _resultinternal.Dispose();
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        #endregion
    }
}

public class DBParameter : IDisposable
{

    private string _parametername;
    private ParameterDirection _parameterdirection;
    private SqlDbType _datatype;
    private object _value;
    private int _size = 0;

    #region "Constructor & Distructor"
    private bool disposed = false;
    public DBParameter()
    {
    }

    public DBParameter(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype, int fieldsize)
    {
        _parametername = parametername;
        _parameterdirection = parameterdirection;
        _datatype = datatype;
        _value = value;
        _size = fieldsize;
    }

    public DBParameter(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
    {
        _parametername = parametername;
        _parameterdirection = parameterdirection;
        _datatype = datatype;
        _value = value;
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

    ~DBParameter()
    {
        Dispose(false);
    }
    #endregion

    #region "Properties"
    public string ParameterName
    {
        get { return _parametername; }
        set { _parametername = value; }
    }

    public ParameterDirection ParameterDirection
    {
        get { return _parameterdirection; }
        set { _parameterdirection = value; }
    }

    public SqlDbType DataType
    {
        get { return _datatype; }
        set { _datatype = value; }
    }

    public object Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public int Size
    {
        get { return _size; }
        set { _size = value; }
    }
    #endregion
}

public class DBParameters : IDisposable
{
    protected ArrayList _innerList;

    #region "Constructor & Distructor"
    private bool disposed = false;
    public DBParameters()
    {
        _innerList = new ArrayList();
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

    ~DBParameters()
    {
        Dispose(false);
    }
    #endregion

    public int Count
    {
        get { return _innerList.Count; }
    }

    public void Add(DBParameter item)
    {
        _innerList.Add(item);
    }

    public int Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype, int fieldsize)
    {
        DBParameter item = new DBParameter(parametername, value, parameterdirection, datatype, fieldsize);
        return _innerList.Add(item);
    }

    public int Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
    {
        DBParameter item = new DBParameter(parametername, value, parameterdirection, datatype);
        return _innerList.Add(item);
    }

    public bool Remove(DBParameter item)
    {
        bool result = false;
        DBParameter obj;

        for (int i = 0; i < _innerList.Count; i++)
        {
            obj = new DBParameter();
            obj = (DBParameter)_innerList[i];
            if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
            {
                _innerList.RemoveAt(i);
                result = true;
                break;
            }
            obj = null;
        }
        return result;
    }

    public bool RemoveAt(int index)
    {
        bool result = false;
        _innerList.RemoveAt(index);
        result = true;
        return result;
    }

    public void clear()
    {
        _innerList.Clear();
    }
    public DBParameter this[int index]
    {
        get
        {
            return (DBParameter)_innerList[index];
        }
    }

    public bool Contains(DBParameter item)
    {
        return _innerList.Contains(item);
    }

    public int IndexOf(DBParameter item)
    {
        return _innerList.IndexOf(item);
    }

    public void copyTo(DBParameter[] array, int index)
    {
        _innerList.CopyTo(array, index);
    }
}