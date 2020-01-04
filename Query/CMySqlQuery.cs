using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlQuery : CMySqlBase, IMySql<DataTable>
    {
        public CMySqlQuery(string connectionString, string sql, List<MySqlParameter> _params, Action<Exception, string> onError)
                         : base(connectionString, sql, _params, onError)
        {
        }

        public DataTable Execute()
        {
            DataTable o_table = null;

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();

                if (_params != null)
                {
                    for (int i = 0; i < _params.Count; i++)
                        command.Parameters.Add(_params[i]);
                }

                using (MySqlDataAdapter tbl = new MySqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();
                    tbl.Fill(ds);

                    o_table = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex, sql);
            }
            finally
            {
                connection.Close();
            }

            return o_table;

        }

        public async Task<DataTable> ExecuteAsync()
        {
            DataTable o_table = null;

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                await connection.OpenAsync();

                if (_params != null)
                {
                    for (int i = 0; i < _params.Count; i++)
                        command.Parameters.Add(_params[i]);
                }

                using (MySqlDataAdapter tbl = new MySqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();
                    await tbl.FillAsync(ds);

                    o_table = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex, sql);
            }
            finally
            {
                connection.Close();
            }

            return o_table;

        }
    }
}

