using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_Analyzer.BNF_Engine;

namespace ACS_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            //ACS_Parser.Parser p=new ACS_Parser.Parser(ACS_Lexer.Lexer._Main());
            BNF.Match(ACS_Lexer.Lexer._Main());
            Console.ReadKey();
        }
    }
}
