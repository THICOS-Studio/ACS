using ACS_Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        //   for (now_run_id = 0; now_run_id < q.Count-1; )
        //   {
        //      Console.WriteLine("（1）>> [ 变量"+q[now_run_id].GetValue()+ "赋值" + GetExper(q[now_run_id]).Calculate() + "]");
                //Console.WriteLine("L++"+now_run_id);
         //   }
            //Console.Read();
        }
        public void Start()
        {
            for (now_run_id = 0; now_run_id < q.Count - 1;)
           {
                //Console.WriteLine("（1）>> [ 变量"+q[now_run_id].GetValue()+ "赋值" +
                GetExper(q[now_run_id]).Calculate();
                  //+ "]");
           
           }
        }

        public string Test()
        {
          return  GetExper(q[now_run_id]).Calculate();
        }

        public BinaryExper GetExper(Token t)
        {
          

            bool debug=false;
            if (t == null)
            {
                return new BinaryExper("");
            }

            if (t.GetValue() == "print")
            {
                if (q[t.seq + 1].GetValue() == "(")
                {
                    if (q[t.seq + 3].GetValue() == ")" && q[t.seq + 4].GetValue() == ";")
                    {
                        if(q[t.seq+2].type==Types.Identifier)
                        Console.WriteLine(GetVar( q[t.seq + 2].GetValue()));
                        else
                        Console.WriteLine(q[t.seq + 2].GetValue().Replace("\"",""));
                        now_run_id = t.seq + 5;
                        return new BinaryExper("");
                    }
                }
            }
            else if (t.GetValue() == "[" && q[t.seq + 2].GetValue() == "]" && q[t.seq + 3].GetValue() == ";")
            {
                SetVar(q[t.seq + 1].GetValue(), (t.seq + 2).ToString());
                now_run_id = t.seq + 4;
                return new BinaryExper("");
            }
            else if (t.GetValue() == "goto" && q[t.seq + 2].GetValue() == ";")
            {
                now_run_id =int.Parse(GetVar(q[t.seq + 1].GetValue()));
                return new BinaryExper("");
            }
            else if (t.GetValue() == "quit" && q[t.seq + 1].GetValue() == "（"&&q[t.seq + 2].GetValue() == ")" && q[t.seq + 3].GetValue() == ";")
            {
                Environment.Exit(0);
                return new BinaryExper("");
            }
            else if (t.GetValue() == "clear" && q[t.seq + 1].GetValue() == "（" && q[t.seq + 2].GetValue() == ")" && q[t.seq + 3].GetValue() == ";")
            {
                Console.Clear();
                return new BinaryExper("");
            }
            else if (t.GetValue() == "input")
            {
                if (q[t.seq + 1].GetValue() == "(")
                {
                    if (q[t.seq + 3].GetValue() == ")" && q[t.seq + 4].GetValue() == ";")
                    {
                        if (q[t.seq + 2].type == Types.Identifier) {
                            string name = q[t.seq + 2].GetValue();
                         if (!var_name.Contains(name))
                            {
                                var_name.Add(name);
                                var_data.Add("");
                            }

                            var_data[var_name.IndexOf(name)] = Console.ReadLine(); ;
                            now_run_id = t.seq + 5;
                            return new BinaryExper("");
                        }
                    }
                }
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
          
          if (t.GetValue() == "(")
            {
                int LP_count = 1;
                List<Token> inter_tokens=new List<Token>();
                inter_tokens.Add(new OperatorToken(0, inter_tokens.Count, "KEEP"));
                inter_tokens.Add(new OperatorToken(0, inter_tokens.Count, "="));
                for (int i = 1;i<q.Count-1; i++)
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
                inter_tokens.Add(new OperatorToken(0, inter_tokens.Count, ";"));

                BinaryExper vvv = new BinaryExper(GetValueFromTokens(inter_tokens));
                if (q.Count > (t.seq + inter_tokens.Count + 1))
                {
                    now_exper.Left = vvv;
                    //Console.WriteLine((t.seq + inter_tokens.Count + 1) + "---" + q.Count);
                    now_exper.Operator = q[t.seq + inter_tokens.Count + 1].GetValue();
                    now_exper.Right = GetExper(q[t.seq + inter_tokens.Count + 2]);
                }
                else
                {
                    now_exper = vvv;
                }
                return now_exper;
            }         
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
                new_exper.Right= GetExper(q[t.seq + 4]);
                if (debug) Console.WriteLine("D" + new_exper.value);
                return new_exper;
            }
            else
            {
                //非乘除
                now_exper.Right = GetExper(q[t.seq + 2]);
                if (debug) Console.WriteLine("E" + now_exper.value);
                return now_exper;
            }
            //递归走起

        }

        public string GetVar(string s)
        {
            if (!var_name.Contains(s)) return "ERROR";
           return var_data[var_name.IndexOf(s)];
        }
        public void SetVar(string name,string value)
        {
            if (var_name.Contains(name))
            {
                var_data[var_name.IndexOf(name)]=value;
            }
            else
            {
                var_name.Add(name);
                var_data.Add(value);
            }
        }






        //下面的函数想要做一个小方法能获取 某token集合的值//但是失败了，如果可以成功的话括号就能做出来了
        //下面是重新调用GetExper函数，导致很多错误，重写可能负责

        int child_run_id=0;
        public string GetValueFromTokens(List<Token> t)
        {
                Parser p = new Parser(t);
                 string result = p.Test();
                instance = this;
                 return result;

           // return GetExper2(t[0], t).Calculate();
        }
   
    }





    }

