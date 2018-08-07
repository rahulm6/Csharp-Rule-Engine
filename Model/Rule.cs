using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine.Model
{
    public class Rule
    {
            public string value_type { get; set; }
            public string signal { get; set; }

            public string Operator { get; set; }

            public string value { get; set; }

            public Rule(string signal, string Operator, string value, string value_type)
            {
                this.signal = signal;
                this.Operator = Operator;
                this.value = value;
                this.value_type = value_type;
            }
        }
}
