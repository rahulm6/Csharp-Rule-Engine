using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Utility
{
    public static class RuleValidator
    {
        #region Operators

        public static bool s_opGreaterThan(object o1, object o2)
        {
            if (o1 == null || o2 == null || o1.GetType() != o2.GetType() || !(o1 is IComparable))
                return false;
            return (o1 as IComparable).CompareTo(o2) > 0;
        }

        public static bool s_opEqual(object o1, object o2)
        {
            return o1 == o2;
        }

        public static bool s_opNotEqual(object o1, object o2)
        {
            return o1 != o2;
        }



        #endregion
    }
}
