using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    public class CCondition
    {
        public string ConditionalOperator { get; private set; }//OR AND 
        public string ColName { get; private set; }
        public object ColValue { get; private set; }
        public string Operator { get; private set; } // == != > < 

        public CCondition(string conditionalOperator, string colName, object colValue, string _operator)
        {
            ConditionalOperator = conditionalOperator;
            ColName = colName;
            ColValue = colValue;
            Operator = _operator;
        }
    }
}
