using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libMySqlData
{
    /// <summary>
    ///  response object for INSERT, UPDATE, DELETE statements
    /// </summary>
    public class CDdlReturnValue
    {
        public bool Succeeded { get; set; }
        public int LastInsertedId { get; set; }//last identity number
        public int AffectedRows { get; set; }//number of rows affected by action
        public string ValidationErrorMsg { get; set; }
        public string CustomReturnValue { get; set; }
    }
}
