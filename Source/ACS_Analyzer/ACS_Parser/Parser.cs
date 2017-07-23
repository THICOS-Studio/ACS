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
        }

        public void Start()
        {
            for (now_run_id = 0; now_run_id < q.Count - 1;)
           {
                Console.WriteLine(">> [ 变量"+q[now_run_id].GetValue()+ "赋值" + GetExper(q[now_run_id]).Calculate()+ "]");
                //GetExper(q[now_run_id]).Calculate();
            }
        }

        public string Test()
        {
          return  GetExper(q[now_run_id]).Calculate();
        }

        public BinaryExper GetExper(Token t)
        {
            Console.WriteLine("检测到"+t.GetValue());
            if (t == null) //如果t是空则返回空值
            {
                return new BinaryExper("");
            }
            #region 保留字段的函数检测
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

            #endregion
 
            if (t.seq < q.Count)
            {
                if (q[t.seq + 1].GetValue() == ";")
                {
                    now_run_id = t.seq + 2;
                    BinaryExper e = new BinaryExper(t);
                    return e;
                }
            }
            else
            {
                BinaryExper e = new BinaryExper(t);
                return new BinaryExper(e);
            }
            BinaryExper now_exper=new BinaryExper();

            #region 括号
            if (t.GetValue() == "(")
            {
                Console.WriteLine("检测到左括号");

                   int LP_count = 1;
                List<Token> inter_tokens=new List<Token>();
                inter_tokens.Add(new OperatorToken(0, inter_tokens.Count, "KEEP"));
                inter_tokens.Add(new OperatorToken(0, inter_tokens.Count, "="));

                Console.WriteLine("分析递归内数据*************************");
                for (int i = 0; i < q.Count-1; i++)
                {
                    Console.Write(q[i].GetValue());
                }
                Console.WriteLine();
                Console.WriteLine("分析递归内数据结束*************************");
                for (int i = 1;i<q.Count-1; i++)
                {
                    Console.WriteLine("括号内循环检测"+"(" +i+"/"+q.Count+")"+":" + q[t.seq + i].GetValue());
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
                        Token nt = q[t.seq + i];
                        nt.seq = inter_tokens.Count;
                       
                        if (LP_count == 0)
                        {
                            break;
                        }
                        else {
                            inter_tokens.Add(nt);
                        }
                    }
                }
                Token nt2 = new OperatorToken(0, inter_tokens.Count,";");nt2.seq = inter_tokens.Count;inter_tokens.Add(nt2);

                BinaryExper vvv = new BinaryExper(GetValueFromTokens(inter_tokens));
                vvv.Calculate();
               
                if (q.Count > (t.seq + inter_tokens.Count + 1))
                {
                    now_exper.Left = vvv;
                    now_exper.Operator = q[t.seq + inter_tokens.Count -1].GetValue();
                    if (now_exper.Operator == ";")
                    {
                        Console.WriteLine("返回异常分支");
                        now_run_id = t.seq + inter_tokens.Count;
                        now_exper = vvv;
                    }
                    else {
                        now_exper.Right = GetExper(q[t.seq + inter_tokens.Count]);
                    }
                    Console.WriteLine("返回" + now_exper.Calculate());
                    return now_exper;
                }
                else
                {
                    Console.WriteLine("返回异常分支");
                    now_exper = vvv;
                         return now_exper;
                }
               
            }
            #endregion
            now_exper.Left = new BinaryExper(t);
            now_exper.Operator = q[t.seq + 1].GetValue();
            if (now_exper.Operator == "*" | now_exper.Operator == "/" | now_exper.Operator == "%")
            {
                now_exper.Right = new BinaryExper(q[t.seq + 2]);
                if (q[t.seq + 3].GetValue() == ";")
                {
                    now_run_id = t.seq + 4;
                    return now_exper;
                }
                BinaryExper new_exper = new BinaryExper();
                new_exper.Left = now_exper;
                if (q[t.seq + 2].GetValue() == "(")
                {
                    new_exper.Operator = q[t.seq + 1].GetValue();
                    new_exper.Right = GetExper(q[t.seq + 2]);
                }
                else {
                 
                    new_exper.Operator = q[t.seq + 3].GetValue();
                    new_exper.Right = GetExper(q[t.seq + 4]);
                }
                return new_exper;
            }
            else
            {
                now_exper.Right = GetExper(q[t.seq + 2]);
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


        public string GetValueFromTokens(List<Token> t)
        {
                Parser p = new Parser(t);
                 string result = p.Test();
                instance = this;
                 return result;
        }
   
    }
}

