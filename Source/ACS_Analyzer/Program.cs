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
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();  //开始监视代码运行时间
            //ACS_Parser.Parser p=new ACS_Parser.Parser(ACS_Lexer.Lexer._Main());
            BNF.Match(ACS_Lexer.Lexer._Main());
            watch.Stop();
            TimeSpan timespan = watch.Elapsed;
            Console.WriteLine("执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
            Console.ReadKey();

        }
    }
}
