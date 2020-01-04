using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace libMySqlData
{
    public static class CGetQueryParams
    {
        public static List<MySqlParameter> Get(Dictionary<string, object> dic)
        {
            List<MySqlParameter> _params = new List<MySqlParameter>();

            if (dic != null)
            {
                foreach (KeyValuePair<string, object> item in dic)
                    _params.Add(new MySqlParameter(item.Key, item.Value));
            }

            return _params;
        }
        public static List<MySqlParameter> Get(List<Dictionary<string, object>> list)
        {
            List<MySqlParameter> _params = new List<MySqlParameter>();

            for (int i = 0; i < list.Count; i++)
            {
                foreach (KeyValuePair<string, object> item in list[i])
                    _params.Add(new MySqlParameter(item.Key + "_" + i, item.Value));
            }



            return _params;
        }
        public static List<MySqlParameter> Get(List<CCondition> whereConditions)
        {
            List<MySqlParameter> _params = new List<MySqlParameter>();

            if (whereConditions != null)
            {
                for (int i = 0; i < whereConditions.Count; i++)
                    _params.Add(new MySqlParameter(whereConditions[i].ColName, whereConditions[i].ColValue));
            }

            return _params;
        }
    }
}
