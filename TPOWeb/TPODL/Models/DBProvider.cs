using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.DL.Framework
{
    /// <summary>
    /// Used to perform operations directly against the database.
    /// </summary>
    public class DBProvider
    {
        #region Variables
        #endregion

        #region Constants
        private const string DEFAULT_CONNECTION_NAME = "";
        #endregion

        #region Properties

        /// <summary>
        /// The name of the connection string in the configuration file.
        /// </summary>
        private string ConnectionStringName { get; set; }

        /// <summary>
        /// The connection string found in the configuration file matching the supplied connection string name.
        /// </summary>
        private string ConnectionString 
        {
            get
            {
                System.Configuration.ConnectionStringSettings connectionStringSetting = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName];
                return connectionStringSetting != null ? connectionStringSetting.ConnectionString : string.Empty;
            }
        }

        /// <summary>
        /// The connection used to connect to the database.
        /// </summary>
        private SqlConnection SQLConnection { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new DBProvider object using an optionally specified connection string name.
        /// </summary>
        /// <param name="connectionStringName">The name of the connection string to use.  If nothing is specified, the default connection string is used.</param>
        public DBProvider(string connectionStringName = DEFAULT_CONNECTION_NAME) 
        {
            ConnectionStringName = connectionStringName;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the named stored procedure with the specified parameters (if any).
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <param name="parameters">A list of parameters to pass to the stored procedure.</param>
        /// <returns>A SqlDataReader containing any results from the stored procedure, if a return value is expected.</returns>
        public SqlDataReader ExecuteStoredProcedure(string storedProcedureName, List<SqlParameter> parameters) 
        {
            Exception executionError = null;
            SqlDataReader dataReader = null;
            SqlCommand command = null;

            //Open the database connection
            OpenConnection();
            
            try
            {
                //Create a new command with the specified stored procedure name
                command = new SqlCommand(storedProcedureName, SQLConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Add in the parameters
                for (int i = 0; i < parameters.Count; i++)
                {
                    command.Parameters.Add(parameters[i]);
                }

                dataReader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                //Assign the exception so it can be thrown after we've disposed the SqlCommand and closed our database connection
                executionError = ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
                CloseConnection();
            }

            //Throw the exception if there was one
            if (executionError != null) 
            {
                throw executionError;
            }

            return dataReader;
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        /// <summary>
        /// Opens a connection to the database using the named connection string.
        /// </summary>
        /// <returns>Returns true if the connection was successfully opened, otherwise returns false.</returns>
        private void OpenConnection() 
        {
            if (string.IsNullOrEmpty(ConnectionString)) 
            {
                throw new Exception(string.Format("Connection String {0} not found in configuration file.", ConnectionStringName));
            } else
            {
                try
                {
                    SQLConnection = new SqlConnection(ConnectionString);
                    SQLConnection.Open();
                }
                catch (Exception ex) 
                {
                    SQLConnection.Dispose();
                    SQLConnection = null;
                    throw ex;
                }
            }

        }

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        private void CloseConnection() 
        {
            if (SQLConnection != null)
            {
                SQLConnection.Dispose();
                SQLConnection = null;
            }
        }
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}
