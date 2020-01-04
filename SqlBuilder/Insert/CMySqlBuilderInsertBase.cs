using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData.SqlBuilder.Insert
{
    public abstract class CMySqlBuilderInsertBase
    {
        protected string tableName;
        protected Dictionary<string, object> keyValue;
    }
}
