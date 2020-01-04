using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData.Query.GroupByWithUnion
{
    public class GroupByWithUnionSubQuery
    {
        string table;
        bool countMode;
        bool countDistinct;
        public string AsName { get; private set; }
        string wherePart;
        string resultByColumn;
        List<string> groupByColumns;
        public string GroupByColumnsSql { get; private set; }


        public GroupByWithUnionSubQuery(string table, bool countMode, bool countDistinct, string asName, string wherePart, string resultByColumn, List<string> groupByColumns)
        {
            this.table = table;
            this.countMode = countMode;
            this.countDistinct = countDistinct;
            AsName = asName;
            this.wherePart = wherePart;
            this.resultByColumn = resultByColumn;
            this.groupByColumns = groupByColumns;

            SetGroupByColumnsSql();
        }

        void SetGroupByColumnsSql()
        {
            StringBuilder stringBuilder = new StringBuilder(500);

            for (int i = 0; i < groupByColumns.Count; i++)
            {
                if (i != 0)
                    stringBuilder.Append(",");

                stringBuilder.Append(groupByColumns[i]);
            }

            GroupByColumnsSql = stringBuilder.ToString();
        }
        public string GetSql(List<string> columnsNames,string extraSelectColumn)
        {
            StringBuilder stringBuilder = new StringBuilder(500);

            stringBuilder.Append(" SELECT");

            for (int i = 0; i < columnsNames.Count; i++)
            {
                if (i != 0)
                    stringBuilder.Append(",");

                if (columnsNames[i] == AsName)
                {
                    if (countMode)
                    {
                        stringBuilder.Append(" COUNT(");

                        if (countDistinct)
                            stringBuilder.Append("DISTINCT ");
                    }

                    else
                        stringBuilder.Append(" SUM(");

                    stringBuilder.Append(resultByColumn);

                    stringBuilder.Append(") AS ");
                    stringBuilder.Append(AsName);
                }
                else
                {
                    stringBuilder.Append(" 0 AS ");
                    stringBuilder.Append(columnsNames[i]);
                }
            }

       
       
            

            stringBuilder.Append(" ,");
            stringBuilder.Append(GroupByColumnsSql);

            if (extraSelectColumn != null)
            {
                stringBuilder.Append(" ,");
                stringBuilder.Append(extraSelectColumn);
            }



                stringBuilder.Append(" FROM ");

            stringBuilder.Append(table);

            if (wherePart != null)
                stringBuilder.Append(wherePart);

            stringBuilder.Append(" GROUP BY ");
            stringBuilder.Append(GroupByColumnsSql);

            if (extraSelectColumn != null)
            {
                stringBuilder.Append(" ,");
                stringBuilder.Append(extraSelectColumn);
            }

            return stringBuilder.ToString();
        }
    }
}
