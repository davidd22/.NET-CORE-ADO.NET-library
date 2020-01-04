using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace libMySqlData
{
    public abstract class CMySqlBase
    {
        protected string connectionString;
        protected string sql;
        protected List<MySqlParameter> _params;
        protected Action<Exception, string> onError;

        protected CMySqlBase(string connectionString, string sql, List<MySqlParameter> _params, Action<Exception, string> onError)
        {
            this.connectionString = connectionString;
            this.sql = sql;
            this._params = _params;
            this.onError = onError;
        }
    }
}
