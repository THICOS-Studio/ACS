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

        static  void Main(string[] args)
         {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();  //开始监视代码运行时间
            ACS_Parser.VariableList vars = new ACS_Parser.VariableList();
            Console.WriteLine("****************下面是程序内容******************");
            ACS_Parser.Parser p=new ACS_Parser.Parser(ACS_Lexer.Lexer._Main());
            p.Start();
            Console.WriteLine("*****************程序内容结束*******************");
            //   BNF.Match(ACS_Lexer.Lexer._Main());
            watch.Stop();
             TimeSpan timespan = watch.Elapsed;
              Console.WriteLine("执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
                Console.ReadKey();
        }


    }
}
