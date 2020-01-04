using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace libMySqlData
{
    public class CMySqlDdl : CMySqlBase, IMySql<CDdlReturnValue>
    {
        bool getNewInsertId;
        public CMySqlDdl(string connectionString, string sql, List<MySqlParameter> _params
                       , Action<Exception, string> onError, bool getNewInsertId = false)

                      : base(connectionString, sql, _params, onError)
        {
            this.getNewInsertId = getNewInsertId;
        }

        public CDdlReturnValue Execute()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();
            MySqlCommand command = null;

            MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = connectionString
            };


            try
            {
                connection.Open();
                command = new MySqlCommand(sql, connection);

                if (_params != null)
                {
                    for (int i = 0; i < _params.Count; i++)
                        command.Parameters.Add(_params[i]);

                }

                cDdlReturnValue.AffectedRows = command.ExecuteNonQuery();

                if (getNewInsertId)
                    cDdlReturnValue.LastInsertedId = (int)command.LastInsertedId;


                cDdlReturnValue.Succeeded = cDdlReturnValue.AffectedRows > 0;
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex, sql);

            }
            finally
            {
                connection.Dispose();
                connection.Close();

                command.Dispose();
            }


            return cDdlReturnValue;
        }
        public async Task<CDdlReturnValue> ExecuteAsync()
        {
            CDdlReturnValue cDdlReturnValue = new CDdlReturnValue();
            MySqlCommand command = null;

            MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = connectionString
            };


            try
            {

                await connection.OpenAsync();
                command = new MySqlCommand(sql, connection);

                if (_params != null)
                {
                    for (int i = 0; i < _params.Count; i++)
                        command.Parameters.Add(_params[i]);
                }

                cDdlReturnValue.AffectedRows = await command.ExecuteNonQueryAsync();

                if (getNewInsertId)
                    cDdlReturnValue.LastInsertedId = (int)command.LastInsertedId;


                cDdlReturnValue.Succeeded = cDdlReturnValue.AffectedRows > 0;

            }
            catch (Exception ex)
            {
                onError?.Invoke(ex, sql);

            }
            finally
            {
                connection.Dispose();
                connection.Close();

                command.Dispose();
            }


            return cDdlReturnValue;
        }
    }
}

