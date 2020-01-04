using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderInsertOrUpdateOnDuplicateKey : CMySqlBuilder, IMySqlBuilder
    {
        string onDuplicateUpdateStatement;

        public CMySqlBuilderInsertOrUpdateOnDuplicateKey(string tableName
                                                       , Dictionary<string, object> keyValue
                                                       , string onDuplicateUpdateStatement)
                    : base(tableName, keyValue)
        {
            this.onDuplicateUpdateStatement = onDuplicateUpdateStatement;
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

                stringBuilder.Append("@" + item.Key);

                currentValueNumber++;
            }

            stringBuilder.Append(")");

            stringBuilder.Append(" ON DUPLICATE KEY UPDATE ");
            stringBuilder.Append(onDuplicateUpdateStatement);

            return stringBuilder.ToString();
        }
    }
}
