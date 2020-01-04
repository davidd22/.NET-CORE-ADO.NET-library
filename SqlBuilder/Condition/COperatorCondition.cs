using System;
using System.Collections.Generic;
using System.Text;

namespace libMySqlData
{
    public static class COperatorCondition
    {
        public const string AND = "AND";
        public const string OR = "OR";

        public const string NOT = "NOT";
        public const string IN = "IN";

        public const string LIKE = "LIKE";
        public const string EQUAL = "=";
        public const string NOT_EQUAL = "!=";
        public const string GREATER_THAN = ">";
        public const string GREATER_THAN_OR_EQUAL = ">=";
        public const string LESS_THAN = "<";
        public const string LESS_THAN_OR_EQUAL = "<=";

        public const string IS_NULL = "IS NULL";
        public const string IS_NOT_NULL = "IS NOT NULL";
    }
}
