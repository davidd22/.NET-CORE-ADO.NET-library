using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderInsertBulk : CMySqlBuilderBulk, IMySqlBuilder
    {
        public CMySqlBuilderInsertBulk(string tableName
                                     , List<Dictionary<string, object>> keyValues) 
                                        : base(tableName, keyValues)
        {

        }
        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("INSERT INTO ");

            stringBuilder.Append(tableName);
            stringBuilder.Append("(");

            int currentKeyNumber = 0;



            foreach (KeyValuePair<string, object> item in keyValues[0])
            {
                if (currentKeyNumber != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append(item.Key);

                currentKeyNumber++;
            }


            stringBuilder.Append(")");
            stringBuilder.Append("VALUES");

            for (int i = 0; i < keyValues.Count; i++)
            {
                int currentValueNumber = 0;
                stringBuilder.Append("(");

                foreach (KeyValuePair<string, object> item in keyValues[i])
                {
                    if (currentValueNumber != 0)
                        stringBuilder.Append(",");

                    stringBuilder.Append("@");
                    stringBuilder.Append(item.Key);

                    stringBuilder.Append("_");
                    stringBuilder.Append(i);
                    currentValueNumber++;
                }

                stringBuilder.Append(")");

                if (i + 1 == keyValues.Count)
                    stringBuilder.Append(";");
                else
                    stringBuilder.Append(",");
            }

            return stringBuilder.ToString();
        }
    }
}
