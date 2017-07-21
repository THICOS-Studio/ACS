using ACS_Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ACS_Analyzer.ACS_Parser
{
    
    class Parser
    {
        public static Parser instance;
        public List<Token> q;
        public int now_run_id=0;

        public List<string> var_name = new List<string>();
        public List<string> var_data = new List<string>();
        public Parser(List<Token> _q)
        {
            instance = this;
            q = _q;

            /*  if((q[0].type.ToString())== "Identifier")
              {
                  BinaryExper b = new BinaryExper("", new BinaryExper(q[0].GetValue(), new BinaryExper(q[2].GetValue())))
                  {
                      Operator = q[1].GetValue()
                  };
                  Console.WriteLine(b.Operator);
                  b.Calculate();
                  Console.WriteLine(b.value);
              }
              else
              {
                  Console.WriteLine("error");
                  //error
              }*/

           for (now_run_id = 0; now_run_id < q.Count-1; )
           {
              Console.WriteLine("（1）>> [ 变量"+q[now_run_id].GetValue()+ "赋值" + GetExper(q[now_run_id]).Calculate() + "]");
                //Console.WriteLine(now_run_id);
            }
           
            //Console.Read();
        }



        public BinaryExper GetExper(Token t)
        {
            
            if (q[t.seq + 1].GetValue() == ";")
            {
                now_run_id = t.seq + 2;
                BinaryExper e = new BinaryExper(t);
                return e;
            }
            BinaryExper now_exper=new BinaryExper();
            now_exper.Left = new BinaryExper(t);
            now_exper.Operator = q[t.seq + 1].GetValue();
            if (now_exper.Operator == "*" | now_exper.Operator == "/" | now_exper.Operator == "%")
            {
                //乘除优先级
                now_exper.Right = new BinaryExper(q[t.seq + 2]);
                if (q[t.seq + 3].GetValue() == ";")
                {
                    now_run_id = t.seq + 4;
                    return now_exper;
                }
                BinaryExper new_exper = new BinaryExper();
                new_exper.Left = now_exper;
                new_exper.Operator = q[t.seq + 3].GetValue();
                new_exper.Right= GetExper(q[t.seq + 4]);
                return new_exper;
            }
            else
            {
                //非乘除
                now_exper.Right = GetExper(q[t.seq + 2]);
               // Console.WriteLine(now_exper.Left.Calculate()+"  "+now_exper.Operator+"  "+now_exper.Right.Calculate());
                return now_exper;

            }
            //递归走起

        }


    }
}
