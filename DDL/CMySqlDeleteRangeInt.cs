using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlDeleteRangeInt : CMySqlDdlBase, IMySqlDdl
    {
        string rangeColumn;
        int[] rangeInt;

        public CMySqlDeleteRangeInt(string connectionString
                          , string tableName
                          , string rangeColumn
                          , int[] rangeInt
                          , Action<Exception, string> onError = null
                          , bool updateOnDuplicateValues = false) : base(connectionString, tableName, null, onError)
        {
            this.rangeColumn = rangeColumn;
            this.rangeInt = rangeInt;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderDeleteRange CMySqlBuilderDeleteRange = new CMySqlBuilderDeleteRange(tableName, rangeColumn, rangeInt);

            parsedSql = CMySqlBuilderDeleteRange.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                //  _params.AddRange(CGetQueryParams.Get(whereConditions));

                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, parsedSql, _params, onError);

                cDdlReturnValue = cMySqlDdl.Execute();
            }

            return cDdlReturnValue;
        }
        public async Task<CDdlReturnValue> ExecuteAsync()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            CMySqlBuilderDeleteRange CMySqlBuilderDeleteRange = new CMySqlBuilderDeleteRange(tableName, rangeColumn, rangeInt);

            parsedSql = CMySqlBuilderDeleteRange.Build();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                List<MySqlParameter> _params = CGetQueryParams.Get(keyValue);
                // _params.AddRange(CGetQueryParams.Get(whereConditions));

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
