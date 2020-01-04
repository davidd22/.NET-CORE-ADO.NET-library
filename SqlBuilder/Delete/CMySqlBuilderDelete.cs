using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderDelete : CMySqlBuilder, IMySqlBuilder
    {
        List<CCondition> whereConditions;
        public CMySqlBuilderDelete(string tableName
                                 , List<CCondition> whereConditions) : base(tableName, null)
        {
            this.whereConditions = whereConditions;
        }
        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("DELETE FROM ");
          
            stringBuilder.Append(tableName);

            for (int i = 0; i < whereConditions.Count; i++)
            {
                if (i == 0)
                    stringBuilder.Append(" WHERE ");

                if (whereConditions.Count > 1)
                {
                    if (i != 0)
                        stringBuilder.Append(" " + whereConditions[i].ConditionalOperator);
                }

                stringBuilder.Append(" ");
                stringBuilder.Append(whereConditions[i].ColName);
                stringBuilder.Append(whereConditions[i].Operator);
                stringBuilder.Append("@");
                stringBuilder.Append(whereConditions[i].ColName);
            }


            return stringBuilder.ToString();
        }
    }
}
