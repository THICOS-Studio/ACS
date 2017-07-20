using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            ACS_Parser.Parser p=new ACS_Parser.Parser(ACS_Lexer.Lexer._Main());
            Console.Read();
        }
    }
}
