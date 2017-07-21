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
            DateTime dt1 = System.DateTime.Now;


           
            ACS_Parser.Parser p=new ACS_Parser.Parser(ACS_Lexer.Lexer._Main());


            //   BNF.Match(ACS_Lexer.Lexer._Main());
            DateTime dt2 = System.DateTime.Now;
            TimeSpan ts = dt2.Subtract(dt1);

            Console.WriteLine("您的运行速度：{0}", ts.TotalMilliseconds+ "毫秒");
            Console.WriteLine("打败了全球1%的程序！");
            Console.WriteLine("您被fangxm 等9999名开发者击败了");
            Console.ReadKey();
        }
    }
}
