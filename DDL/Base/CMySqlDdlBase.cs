using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public abstract class CMySqlDdlBase
    {
        protected string connectionString;
        protected string tableName;
        protected Dictionary<string, object> keyValue;
        protected Action<Exception, string> onError;
        protected string parsedSql;
        protected CMySqlDdlBase(string connectionString, string tableName, Dictionary<string, object> keyValue, Action<Exception, string> onError)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
            this.keyValue = keyValue;
            this.onError = onError;
        }
    }
}
