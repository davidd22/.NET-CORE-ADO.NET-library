using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlUpdate : CMySqlDdlBase, IMySqlDdl
    {

        List<CCondition> whereConditions;

        public CMySqlUpdate(string connectionString
                          , string tableName
                          , List<CCondition> whereConditions
                          , Dictionary<string, object> keyValue
                          , Action<Exception, string> onError = null
                          , bool updateOnDuplicateValues = false) : base(connectionString, tableName, keyValue, onError)
        {
            this.whereConditions = whereConditions;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderUpdate cMySqlBuilderUpdate = new CMySqlBuilderUpdate(tableName, keyValue, whereConditions);

            parsedSql = cMySqlBuilderUpdate.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                _params.AddRange(CGetQueryParams.Get(whereConditions));

                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError);

                cDdlReturnValue = cMySqlDdl.Execute();
            }

            return cDdlReturnValue;
        }
        public async Task<CDdlReturnValue> ExecuteAsync()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderUpdate cMySqlBuilderUpdate = new CMySqlBuilderUpdate(tableName, keyValue, whereConditions);

            parsedSql = cMySqlBuilderUpdate.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                _params.AddRange(CGetQueryParams.Get(whereConditions));

                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError);

                cDdlReturnValue = await cMySqlDdl.ExecuteAsync();
            }

            return cDdlReturnValue;
        }
        public string Validate()
        {
            if (!parsedSql.ToLower().Contains("where"))
            {
                //prevent update the all table
                return @"You are using safe update mode and you tried to update a table without a WHERE that uses a KEY column.";
            }
            return null;
        }
    }
}
