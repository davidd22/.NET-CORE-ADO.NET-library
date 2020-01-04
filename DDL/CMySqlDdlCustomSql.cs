using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlDdlCustomSql : IMySqlDdl
    {

        string connectionString;
        string sql;
        List<MySqlParameter> _params;

        public CMySqlDdlCustomSql(string connectionString, string sql, List<MySqlParameter> _params)
        {
            this.connectionString = connectionString;
            this.sql = sql;
            this._params = _params;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, sql, _params, null);

                cDdlReturnValue = cMySqlDdl.Execute();
            }

            return cDdlReturnValue;
        }
        public async Task<CDdlReturnValue> ExecuteAsync()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();

            cDdlReturnValue.ValidationErrorMsg = Validate();

            if (cDdlReturnValue.ValidationErrorMsg == null)
            {
                CMySqlDdl cMySqlDdl = new CMySqlDdl(connectionString, sql, _params, null);

                cDdlReturnValue = cMySqlDdl.Execute();
            }

            return cDdlReturnValue;
        }
        public string Validate()
        {
            if (!sql.ToLower().Contains("where"))
            {
                //prevent update the all table
                return @"You are using safe update mode and you tried to update a table without a WHERE that uses a KEY column.";
            }
            return null;
        }
    }
}
