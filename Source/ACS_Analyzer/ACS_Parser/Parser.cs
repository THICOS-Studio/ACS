using ACS_Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_Analyzer.ACS_Parser
{
    
    class Parser
    {
        public List<Token> q;
        public Parser(List<Token> _q)
        {
            q = _q;
            if((q[0].type.ToString())== "Number")
            {
                Console.WriteLine("hi");
                BinaryExper b = new BinaryExper("", new BinaryExper(q[0].GetValue(), new BinaryExper(q[2].GetValue())));
                b.Operator = q[1].GetValue();
                Console.WriteLine(b.Operator);
                b.Calculate();
                Console.WriteLine(b.value);
                Console.Read();
            }
            else
            {
                Console.WriteLine("error");
                //error
            }
        }

    }
}
