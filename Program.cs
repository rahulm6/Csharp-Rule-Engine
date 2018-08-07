using Newtonsoft.Json;
using RuleEngine.Model;
using RuleEngine.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RuleEngine
{
    class Program
    {
        public static Dictionary<string, Func<object, object, bool>> s_operators;
        public static Dictionary<string, PropertyInfo> s_properties;
        static Program()
        {
            s_operators = new Dictionary<string, Func<object, object, bool>>();
            s_operators["greater_than"] = new Func<object, object, bool>(RuleValidator.s_opGreaterThan);
            s_operators["equal"] = new Func<object, object, bool>(RuleValidator.s_opEqual);
            s_operators["not_equal"] = new Func<object, object, bool>(RuleValidator.s_opNotEqual);

            s_properties = typeof(Signal).GetProperties().ToDictionary(propInfo => propInfo.Name);
        }

        public static void Main()
        {
            
            // Load the input file for rule validations 
            List<Signal> signals = new List<Signal>();
            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"raw_data.json")))
            {
                string json = r.ReadToEnd();
                signals = JsonConvert.DeserializeObject<List<Signal>>(json);
            }

            // Load all the rules into the memory
            List<Rule> rules = new List<Rule>();
            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"rules.json")))
            {
                string json = r.ReadToEnd();
                rules = JsonConvert.DeserializeObject<List<Rule>>(json);
            }

            // Validate the rules for the input stream data 

            foreach (var signal in signals)
            {
                var rule = rules.Find(k => k.signal.ToLower() == signal.signal.ToLower() && k.value_type.ToLower() == signal.value_type.ToLower());
                if (rule == null) {
                    Console.WriteLine("There is no rule defined for the signal... New Signal has been notified");
                    continue;
                }
                Console.WriteLine(Program.Apply(signal, rule.Operator, rule.signal, rule.value));
            }

            
            Console.ReadKey();

            
        }

        public static bool Apply(Signal user, string op, string prop, object target)
        {
            return s_operators[op](user.value, target);
        }
        
        private static object GetPropValue(Signal user, string prop)
        {
            PropertyInfo propInfo = s_properties[prop];
            return propInfo.GetGetMethod(false).Invoke(user, null);
        }

        
    }
}
