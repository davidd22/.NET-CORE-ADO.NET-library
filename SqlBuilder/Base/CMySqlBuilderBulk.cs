using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal abstract class CMySqlBuilderBulk
    {
        protected string tableName;
        protected List<Dictionary<string, object>> keyValues;

        protected CMySqlBuilderBulk(string tableName, List<Dictionary<string, object>> keyValues)
        {
            this.tableName = tableName;
            this.keyValues = keyValues;
        }
    }
}
