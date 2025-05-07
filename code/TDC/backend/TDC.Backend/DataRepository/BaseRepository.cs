using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TDC.Backend.DataRepository.Helper;

namespace DataRepository
{
    public class BaseRepository
    {
        public string TableName { get; }

        private const    int               _DEFAULT_TIMEOUT_SEC = 30;
        private readonly ConnectionFactory _connectionFactory;

        protected BaseRepository(ConnectionFactory connectionFactory, string tableName)
        {
            this._connectionFactory = connectionFactory;
            this.TableName          = tableName;
        }

    #region Query

        /// <summary>
        /// Query without parameter.
        /// </summary>
        /// <typeparam name="T">Type of the return object</typeparam>
        /// <param name="commandText">SQL String</param>
        /// <returns>List of given Type</returns>
        protected List<T> Query<T>(string commandText) =>
            this.Query<T>(commandText,
                          new { });

        protected Task<List<T>> QueryAsync<T>(string commandText) =>
            this.QueryAsync<T>(commandText,
                               new { });

        protected List<T> Query<T>(string commandText, object parameter, CommandType commandType = CommandType.Text)
        {
            var result = new List<T>();

            try
            {
                using var connection = this._connectionFactory.GetSqlConnection();
                var temp = connection.Query<T>(commandText, parameter, commandType: commandType)
                                     ?.ToList();

                if (temp != null && temp.Any())
                {
                    result.AddRange(temp);
                }
            }
            catch
            {
                //ToDo: Add logging framework
                //Trace.TraceError($"Error while reading from database (Type = {typeof(T).Name}): ({exception.Message})");
                //Trace.TraceError($"{commandType}: {commandText}");
                //Trace.TraceError($"Parameter: {parameter}");
                throw;
            }

            return result;
        }

        protected async Task<List<T>> QueryAsync<T>(string commandText, object parameter, CommandType commandType = CommandType.Text)
        {
            var result = new List<T>();

            try
            {
                await using var connection = this._connectionFactory.GetSqlConnection();
                var             temp       = await connection.QueryAsync<T>(commandText, parameter, commandType: commandType);

                result.AddRange(temp.ToList());
            }
            catch
            {
                //ToDo: Add logging framework
                //Trace.TraceError($"Error while reading from database (Type = {typeof(T).Name}): ({exception.Message})");
                //Trace.TraceError($"{commandType}: {commandText}");
                //Trace.TraceError($"Parameter: {parameter}");
                throw;
            }

            return result;
        }

    #endregion

    #region Execute

        /// <summary>
        /// Execute SQL without parameter
        /// </summary>
        /// <typeparam name="T">Type of the return object</typeparam>
        /// <param name="sql">SQL String</param>
        /// <returns>Returns count of affected rows.</returns>
        protected int Execute<T>(string sql) =>
            this.Execute<T>(sql,
                            new { });

        protected Task<int> ExecuteAsync<T>(string sql) =>
            this.ExecuteAsync<T>(sql,
                                 new { });

        /// <summary>
        /// Execute SQL or Stored Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL or Stored Procedure</param>
        /// <param name="parameter">Parameter for the SQL or Stored Procedure</param>
        /// <param name="commandType">Text or Stored Procedure</param>
        /// <returns>Returns count of affected rows.</returns>
        protected int Execute<T>(string sql, object parameter, CommandType commandType = CommandType.Text)
        {
            try
            {
                using var connection = this._connectionFactory.GetSqlConnection();
                return connection.Execute(sql,
                                          parameter,
                                          commandTimeout: _DEFAULT_TIMEOUT_SEC,
                                          commandType: commandType);
            }
            catch
            {
                //Trace.TraceError($"Error while writing to database (Type = {typeof(T).Name}): ({exception.Message})");
                //Trace.TraceError($"SQL: {sql}");
                //Trace.TraceError($"Parameter: {parameter}");

                throw;
            }
        }

        protected async Task<int> ExecuteAsync<T>(string sql, object parameter, CommandType commandType = CommandType.Text)
        {
            try
            {
                await using var connection = this._connectionFactory.GetSqlConnection();
                return await connection.ExecuteAsync(sql,
                                                     parameter,
                                                     commandTimeout: _DEFAULT_TIMEOUT_SEC,
                                                     commandType: commandType);
            }
            catch
            {
                //Trace.TraceError($"Error while writing to database (Type = {typeof(T).Name}): ({exception.Message})");
                //Trace.TraceError($"SQL: {sql}");
                //Trace.TraceError($"Parameter: {parameter}");

                throw;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// SQL String or Stored Procedure for Insert. Returns inserted ID.
        /// </summary>
        /// <typeparam name = "T" > Must be of the matching type for the table of Type IDatabaseModel.</typeparam>
        /// <param name = "sql" > SQL string for insert.</param>
        /// <param name = "parameter" > Values to insert</param>
        /// <param name = "commandType">Type of the Command. Default is Text.</param>
        /// <returns>ID of the inserted row</returns>
        protected long Insert<T>(string sql, object parameter, CommandType commandType = CommandType.Text)
        {
            try
            {
                // CHECK: What was returned, if type is stored procedure???
                if (commandType == CommandType.Text)
                {
                    sql = $"{sql}; Select Cast (Scope_Identity() as bigint)";
                }

                using var connection = this._connectionFactory.GetSqlConnection();
                var single = connection.Query<long?>(sql, parameter, commandType: commandType)
                                       .SingleOrDefault();

                return single ?? 0;
            }
            catch
            {
                //Trace.TraceError($"Error while writing to database (Type = {typeof(T).Name}): ({exception.Message})");
                //Trace.TraceError($"{commandType}: {sql}");
                //Trace.TraceError($"Parameter: {parameter}");

                throw;
            }
        }

    #endregion

    #region Table

        protected T GetById<T>(long id)
        {
            var sql = $"SELECT * FROM {this.TableName} WHERE Id = @Id";

            var parameters = new { Id = id };

            return this.Query<T>(sql, parameters)
                       .FirstOrDefault();
        }

        protected List<T> Get<T>()
        {
            if (string.IsNullOrEmpty(this.TableName))
            {
                throw new ArgumentOutOfRangeException(nameof(this.TableName), $"Table name is not specified.");
            }

            var sql = $"SELECT * FROM {this.TableName};";

            return this.Query<T>(sql);
        }

        public bool CleanTable() => this.Execute<int>($"DELETE FROM [dbo].{this.TableName}") > 0;

    #endregion
    }
}