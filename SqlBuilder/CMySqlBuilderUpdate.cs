using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderUpdate : CMySqlBuilder, IMySqlBuilder
    {
        List<CCondition> whereConditions;
        public CMySqlBuilderUpdate(string tableName, Dictionary<string, object> keyValue
                                 , List<CCondition> whereConditions) : base(tableName, keyValue)
        {
            this.whereConditions = whereConditions;
        }
        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("UPDATE ");

            stringBuilder.Append(tableName);
            stringBuilder.Append(" SET ");


            int currentkeyValue = 0;

            foreach (KeyValuePair<string, object> item in keyValue)
            {
                if (currentkeyValue != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append(item.Key);
                stringBuilder.Append("=");
                stringBuilder.Append("@");
                stringBuilder.Append(item.Key);

                currentkeyValue++;
            }

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
