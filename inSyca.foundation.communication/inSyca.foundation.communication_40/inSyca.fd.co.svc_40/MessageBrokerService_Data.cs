using inSyca.foundation.communication.service.diagnostics;
using inSyca.foundation.framework;
using inSyca.foundation.framework.security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace inSyca.foundation.communication.service
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MessageBrokerService
    {
        public System.Data.OleDb.OleDbConnection OleDbSqlConnection_MessageBroker;
        public System.Data.OleDb.OleDbCommand OleDbSqlCommand_MessageBroker;

        public System.Data.OleDb.OleDbConnection OleDbOracleConnection_MessageBroker;
        public System.Data.OleDb.OleDbCommand OleDbOracleCommand_MessageBroker;

        public System.Data.IDataReader DataReader_MessageBroker;

        public SqlBulkCopy sqlBulkCopy;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strConnectionString"></param>
        /// <returns></returns>
        virtual protected bool OpenOleDbSQLConnection(string strConnectionString)
        {
            Log.DebugFormat("OpenOleDbSQLConnection(string strConnectionString {0})", Security.ReplacePasswordCharacters(strConnectionString));

            try
            {
                OleDbSqlConnection_MessageBroker = new System.Data.OleDb.OleDbConnection(strConnectionString);
                OleDbSqlConnection_MessageBroker.Open();
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { Security.ReplacePasswordCharacters(strConnectionString) }, ex));
                return false;
            }

            return true;
        }

        virtual protected bool OpenOleDbOracleConnection(string strConnectionString)
        {
            Log.DebugFormat("OpenOleDbOracleConnection(string strConnectionString {0})", Security.ReplacePasswordCharacters(strConnectionString));

            try
            {
                OleDbOracleConnection_MessageBroker = new System.Data.OleDb.OleDbConnection(strConnectionString);
                OleDbOracleConnection_MessageBroker.Open();
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { Security.ReplacePasswordCharacters(strConnectionString) }, ex));
                return false;
            }

            return true;
        }

        virtual protected bool CloseOleDbSQLConnection()
        {
            Log.Debug("CloseOleDbSQLConnection()");

            if (OleDbSqlConnection_MessageBroker == null)
            {
                Log.DebugFormat("CloseOleDbSQLConnection()\nSqlConnection_MessageBroker not initialized ({0})", OleDbSqlConnection_MessageBroker);
                return true;
            }

            try
            {
                if (OleDbSqlConnection_MessageBroker.State != ConnectionState.Closed)
                    OleDbSqlConnection_MessageBroker.Close();
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                return false;
            }

            return true;
        }

        virtual protected bool CloseOleDbOracleConnection()
        {
            Log.Debug("CloseOleDbOracleConnection()");

            if (OleDbOracleConnection_MessageBroker == null)
            {
                Log.DebugFormat("CloseOleDbOracleConnection()\nOracleConnection_MessageBroker not initialized ({0})", OleDbOracleConnection_MessageBroker);
                return true;
            }

            try
            {
                if (OleDbOracleConnection_MessageBroker.State != ConnectionState.Closed)
                    OleDbOracleConnection_MessageBroker.Close();
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oleDbCommand"></param>
        /// <returns></returns>
        virtual protected bool OleDbExecuteNonQuery(System.Data.OleDb.OleDbCommand oleDbCommand)
        {
            Log.DebugFormat("OleDbExecuteNonQuery(System.Data.OleDb.OleDbCommand oleDbCommand {0})", oleDbCommand);

            try
            {
                oleDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                return false;
            }

            return true;
        }

        virtual protected bool ExecuteSqlBulkCopy(DataTable dataTable, out string errorMessage)
        {
            Log.DebugFormat("ExecuteSqlBulkCopy(DataTable dataTable {0}, out string errorMessage)", dataTable);

            errorMessage = string.Empty;

            try
            {
                sqlBulkCopy.WriteToServer(dataTable);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                if (ex.InnerException != null)
                    errorMessage = string.Format("Error: {0} \n InnerException Message: {1}", ex.Message, ex.InnerException.Message);
                else
                    errorMessage = string.Format("Error: {0} \n", ex.Message);

                return false;
            }

            return true;
        }
    }
}
