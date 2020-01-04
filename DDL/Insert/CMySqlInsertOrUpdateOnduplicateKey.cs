using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlInsertOrUpdateOnduplicateKey : CMySqlDdlBase, IMySqlDdl
    {
        bool getNewInsertId;
        string onDuplicateUpdateStatement;
        public CMySqlInsertOrUpdateOnduplicateKey(string connectionString
                          , string tableName
                          , bool getNewInsertId
                          , string onDuplicateUpdateStatement
                          , Dictionary<string, object> keyValue
                          , Action<Exception, string> onError = null
                          , bool updateOnDuplicateValues = false) : base(connectionString, tableName, keyValue, onError)
        {
            this.getNewInsertId = getNewInsertId;
            this.onDuplicateUpdateStatement = onDuplicateUpdateStatement;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderInsert cMySqlBuilderInsert = new CMySqlBuilderInsert(tableName, keyValue);

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

            CMySqlBuilderInsertOrUpdateOnDuplicateKey cMySqlBuilderInsert = new CMySqlBuilderInsertOrUpdateOnDuplicateKey(tableName, keyValue, onDuplicateUpdateStatement);

            parsedSql = cMySqlBuilderInsert.Build();

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
