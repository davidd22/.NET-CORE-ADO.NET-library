using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlInsert : CMySqlDdlBase, IMySqlDdl
    {
        bool getNewInsertId;

        public CMySqlInsert(string connectionString
                          , string tableName
                          , bool getNewInsertId
                          , Dictionary<string, object> keyValue
                          , Action<Exception, string> onError = null)
                            : base(connectionString, tableName, keyValue, onError)
        {
            this.getNewInsertId = getNewInsertId;
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

            CMySqlBuilderInsert cMySqlBuilderInsert = new CMySqlBuilderInsert(tableName, keyValue);

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
