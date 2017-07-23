using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_Analyzer.BNF_Engine;
using ACS_Lexer;

namespace ACS_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();  //开始监视代码运行时间
            //ACS_Parser.Parser p=new ACS_Parser.Parser(ACS_Lexer.Lexer._Main());
            List<Token> queue = Lexer._Main();
            TimeSpan timespan = watch.Elapsed;
            Console.WriteLine("执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();  //开始监视代码运行时间
            BNF.Match(queue);
            watch.Stop();
            timespan = watch.Elapsed;
            Console.WriteLine("执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
            Console.ReadKey();

        }
    }
}
