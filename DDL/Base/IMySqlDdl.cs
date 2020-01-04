using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    public interface IMySqlDdl
    {
        string Validate();
        CDdlReturnValue Execute();
    }
}
