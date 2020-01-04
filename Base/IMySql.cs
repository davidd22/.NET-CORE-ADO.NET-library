using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    public interface IMySql<T>
    {
        T Execute();
        Task<T> ExecuteAsync();
    }
}
