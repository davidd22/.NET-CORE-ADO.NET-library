using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlInsertBulk : CMySqlDdlBase, IMySqlDdl
    {

        List<Dictionary<string, object>> keyValues;
        public CMySqlInsertBulk(string connectionString
                              , string tableName
                              , List<Dictionary<string, object>> keyValues
                              , Action<Exception, string> onError = null) : base(connectionString, tableName, null, onError)
        {
            this.keyValues = keyValues;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderInsertBulk cMySqlBuilderInsertMany = new CMySqlBuilderInsertBulk(tableName, keyValues);

            parsedSql = cMySqlBuilderInsertMany.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError, false);

                cDdlReturnValue = cMySqlDdl.Execute();
            }

            return cDdlReturnValue;
        }
        public async Task<CDdlReturnValue> ExecuteAsync()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderInsertBulk cMySqlBuilderInsert = new CMySqlBuilderInsertBulk(tableName, keyValues);

            parsedSql = cMySqlBuilderInsert.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValues);
                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError, false);

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
