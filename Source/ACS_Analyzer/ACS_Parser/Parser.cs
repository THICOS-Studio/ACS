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
        public List<Token> Q;
        public int now_run_id=0;
        public List<string> var_name = new List<string>();
        public List<string> var_data = new List<string>();
        public Parser(List<Token> _q)
        {
            instance = this;
            Q = _q;
           for (now_run_id = 0; now_run_id < Q.Count-1; )
           {
              Console.WriteLine("（1）>> [ 变量"+Q[now_run_id].GetValue()+ "赋值" + GetExper(Q[now_run_id],Q).Calculate() + "]");
                //Console.WriteLine("L++"+now_run_id);
            }
            //Console.Read();
        }

        public BinaryExper GetExper(Token t,List<Token> q)
        {
          

            bool debug=false;
            if (t == null)
            {
                return new BinaryExper("");
            }
           // print("检测B" + t.GetValue());
            if (t.seq < q.Count)
            {
                if (q[t.seq + 1].GetValue() == ";")
                {
                    now_run_id = t.seq + 2;
                    BinaryExper e = new BinaryExper(t);
                    if (debug) Console.WriteLine("A"+e.value);
                    return e;
                }
            }
            else
            {
                BinaryExper e = new BinaryExper(t);
                if (debug) Console.WriteLine("B" + e.value+"  "+t.seq+" "+q.Count+" "+t.GetValue());
                return new BinaryExper(e);
            }
            BinaryExper now_exper=new BinaryExper();
          

            //括号代码
          /*  if (t.GetValue() == "(")
            {
                int LP_count = 1;
                List<Token> inter_tokens=new List<Token>();
                for (int i = 1; ; i++)
                {
                    string v = q[t.seq + i].GetValue();
                    if (v != ")")
                    {
                        if (v == "(")
                        {
                            LP_count++;
                        }
                        Token nt = q[t.seq + i];
                        nt.seq = inter_tokens.Count;
                        inter_tokens.Add(nt);
                    } 
					else                                            
                    {
                    LP_count--;
                     if (LP_count == 0)
                        {
                            break;
                        }
                    }
                }
                now_exper.Left = new BinaryExper(GetValueFromTokens(inter_tokens));
                Console.WriteLine("这里");
                now_exper.Operator = q[t.seq + inter_tokens.Count + 1].GetValue();
                now_exper.Right = GetExper(q[t.seq + inter_tokens.Count + 2], Q);
                if (debug) Console.WriteLine("value=" + now_exper.value);
                return now_exper;
            }*/


             now_exper.Left = new BinaryExper(t);
            now_exper.Operator = q[t.seq + 1].GetValue();
            if (now_exper.Operator == "*" | now_exper.Operator == "/" | now_exper.Operator == "%")
            {
                //乘除优先级
                now_exper.Right = new BinaryExper(q[t.seq + 2]);
                if (q[t.seq + 3].GetValue() == ";")
                {
                    now_run_id = t.seq + 4;
                    if (debug) Console.WriteLine("C" + now_exper.value);
                    return now_exper;
                }
                BinaryExper new_exper = new BinaryExper();
                new_exper.Left = now_exper;
                new_exper.Operator = q[t.seq + 3].GetValue();
                new_exper.Right= GetExper(q[t.seq + 4],Q);
                if (debug) Console.WriteLine("D" + new_exper.value);
                return new_exper;
            }
            else
            {
                //非乘除
                now_exper.Right = GetExper(q[t.seq + 2],Q);
                if (debug) Console.WriteLine("E" + now_exper.value);
                return now_exper;
            }
            //递归走起

        }


        //下面的函数想要做一个小方法能获取 某token集合的值//但是失败了，如果可以成功的话括号就能做出来了
        //下面是重新调用GetExper函数，导致很多错误，重写可能负责
        public string GetValueFromTokens(List<Token>t)
        {
            for(int i=0;i<t.Count;i++)
            {
                Console.Write(t[i].GetValue());
            }
            Console.WriteLine("内容");


            return GetExper(t[0], t).Calculate();
        }

    }
}
