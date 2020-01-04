using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal abstract class CMySqlBuilder
    {
        protected string tableName;
        protected Dictionary<string, object> keyValue;

        protected CMySqlBuilder(string tableName, Dictionary<string, object> keyValue)
        {
            this.tableName = tableName;
            this.keyValue = keyValue;
        }
    }
}
