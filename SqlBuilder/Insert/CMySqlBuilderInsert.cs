using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderInsert : CMySqlBuilder, IMySqlBuilder
    {
        public CMySqlBuilderInsert(string tableName
                                , Dictionary<string, object> keyValue)
                                : base(tableName, keyValue)
        {

        }
        public string Build()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("INSERT INTO ");

            stringBuilder.Append(tableName);
            stringBuilder.Append("(");


            int currentKeyNumber = 0;
            int currentValueNumber = 0;

            foreach (KeyValuePair<string, object> item in keyValue)
            {
                if (currentKeyNumber != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append(item.Key);

                currentKeyNumber++;
            }

            stringBuilder.Append(")");
            stringBuilder.Append("VALUES");
            stringBuilder.Append("(");

            foreach (KeyValuePair<string, object> item in keyValue)
            {
                if (currentValueNumber != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append("@");
                stringBuilder.Append(item.Key);

                currentValueNumber++;
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}
