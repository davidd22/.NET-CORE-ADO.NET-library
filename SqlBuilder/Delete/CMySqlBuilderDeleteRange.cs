using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderDeleteRange : CMySqlBuilder, IMySqlBuilder
    {
        string rangeColumn;
        int[] rangeInt;
        public CMySqlBuilderDeleteRange(string tableName
                                   , string rangeColumn
                                   , int[] rangeInt) : base(tableName, null)
        {
            this.rangeColumn = rangeColumn;
            this.rangeInt = rangeInt;
        }
        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("DELETE FROM ");

            stringBuilder.Append(tableName);

            stringBuilder.Append(" WHERE ");
            stringBuilder.Append(rangeColumn);
            stringBuilder.Append(" IN (");

            for (int i = 0; i < rangeInt.Length; i++)
            {

                if (i != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append(rangeInt[i]);

            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}
