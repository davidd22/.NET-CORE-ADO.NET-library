using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    internal class CMySqlBuilderInsertIfNotExists : CMySqlBuilder, IMySqlBuilder
    {
        string whereNotExistsStatement;

        public CMySqlBuilderInsertIfNotExists(string tableName
                                            , Dictionary<string, object> keyValue
                                            , string whereNotExistsStatement)
                                     : base(tableName, keyValue)
        {
            this.whereNotExistsStatement = whereNotExistsStatement;
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
            stringBuilder.Append(" SELECT ");


            foreach (KeyValuePair<string, object> item in keyValue)
            {
                if (currentValueNumber != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append("@" + item.Key);

                currentValueNumber++;
            }



            stringBuilder.Append("  WHERE NOT EXISTS  ");
            stringBuilder.Append("(");
            stringBuilder.Append(whereNotExistsStatement);
            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}
