using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlInsertIfNotExists : CMySqlDdlBase, IMySqlDdl
    {
        bool getNewInsertId;
        string whereNotExistsStatement;

        public CMySqlInsertIfNotExists(string connectionString
                          , string tableName
                          , bool getNewInsertId
                          , string whereNotExistsStatement
                          , Dictionary<string, object> keyValue
                          , Action<Exception, string> onError = null) : base(connectionString, tableName, keyValue, onError)
        {
            this.getNewInsertId = getNewInsertId;
            this.whereNotExistsStatement = whereNotExistsStatement;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderInsertIfNotExists cMySqlBuilderInsert = new CMySqlBuilderInsertIfNotExists(tableName, keyValue, whereNotExistsStatement);

            parsedSql = cMySqlBuilderInsert.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError, getNewInsertId);

                cDdlReturnValue = cMySqlDdl.Execute();
            }

            return cDdlReturnValue;
        }
        public async Task<CDdlReturnValue> ExecuteAsync()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderInsertIfNotExists cMySqlBuilderInsertIfNotExists = new CMySqlBuilderInsertIfNotExists(tableName, keyValue, whereNotExistsStatement);

            parsedSql = cMySqlBuilderInsertIfNotExists.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError, getNewInsertId);

                cDdlReturnValue = await cMySqlDdl.ExecuteAsync();
            }

            return cDdlReturnValue;
        }
        public string Validate()
        {
            return null;
        }
    }
}
