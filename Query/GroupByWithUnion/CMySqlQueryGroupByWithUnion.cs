using libMySqlData.Query.GroupByWithUnion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace libMySqlData.Query.GroupByWithUnion
{
    public class CMySqlQueryGroupByWithUnion : CMySqlBase, IMySql<DataTable>
    {

        List<GroupByWithUnionSubQuery> list;
        List<MySqlParameter> _params;
        string extraGroupBySqlFunction;
        string extraGroupBySqlFunctionColumn;
        public CMySqlQueryGroupByWithUnion(string connectionString
                                           , List<GroupByWithUnionSubQuery> list
                                           , List<MySqlParameter> _params
                                           , string extraGroupBySqlFunction
                                           , string extraGroupBySqlFunctionColumn
                                           , Action<Exception, string> onError)
                                          : base(connectionString, null, null, onError)
        {
            this.list = list;
            this._params = _params;
            this.extraGroupBySqlFunction = extraGroupBySqlFunction;
            this.extraGroupBySqlFunctionColumn = extraGroupBySqlFunctionColumn;
        }

        public DataTable Execute()
        {
            throw new NotImplementedException();
        }
        string GetExtraGroupBySqlFunctionSql()
        {
            return extraGroupBySqlFunction + "(" + extraGroupBySqlFunctionColumn + ") AS extraGroupBySqlFunction";
        }
        public async Task<DataTable> ExecuteAsync()
        {
            StringBuilder stringBuilder = new StringBuilder();

            List<string> sumNames = list.Select(l => l.AsName).Distinct().ToList();

            string extraGroupBySqlFunctionSql = string.Empty;


            if (extraGroupBySqlFunction != null)
                extraGroupBySqlFunctionSql = "," + GetExtraGroupBySqlFunctionSql();

            stringBuilder.Append("SELECT ");

            for (int i = 0; i < sumNames.Count; i++)
            {
                stringBuilder.Append("SUM(");
                stringBuilder.Append(sumNames[i]);

                stringBuilder.Append(")");
                stringBuilder.Append(" AS ");
                stringBuilder.Append(sumNames[i]);

                if (i + 1 < sumNames.Count)
                    stringBuilder.Append(",");
            }


            stringBuilder.Append(extraGroupBySqlFunctionSql);


            stringBuilder.Append(",");

            stringBuilder.Append(list[0].GroupByColumnsSql);

            stringBuilder.Append(" FROM");
            stringBuilder.Append("((");

            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(" UNION");
                    stringBuilder.Append(" (");
                }


                stringBuilder.Append(list[i].GetSql(sumNames, extraGroupBySqlFunctionColumn));

                if (i != 0)
                    stringBuilder.Append(" )");

            }

            stringBuilder.Append(" )) AS RESULTS");

            stringBuilder.Append(" GROUP BY ");

            stringBuilder.Append(list[0].GroupByColumnsSql);


            stringBuilder.Append(extraGroupBySqlFunctionSql.Replace(" AS extraGroupBySqlFunction", string.Empty));


            CMySqlQuery cMySqlQuery = new CMySqlQuery(connectionString
                                                    , stringBuilder.ToString()
                                                    , _params
                                                    , onError
                                                    );

            return await cMySqlQuery.ExecuteAsync();
        }
    }
}
