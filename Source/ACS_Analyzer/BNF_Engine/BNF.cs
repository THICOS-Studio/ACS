using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_Lexer;

namespace ACS_Analyzer.BNF_Engine
{
    public static class BNF
    {
        public static void Match(List<Token> _queue)
        {
            List<Token> input = new List<Token>();
            foreach (Token item in _queue)
            { 
                if (item == null) break;
                if (item.GetValue() == ";")
                {
                    input.Add(item);
                    MatchRules(input);
                    input = new List<Token>();
                    continue;
                }
                input.Add(item);
            }
        }

        static void MatchRules(List<Token> _input)
        {
            Parser parser = new Parser(_input);
            //parser = parser.rule(Types.Identifier).rule("=").rule(Types.Number).rule(";");
            parser = parser.rule(Types.Identifier).rule("=").rule(Types.Number).StartRegion().or("+").or("-").or("*").or("/").rule(Types.Number).EndRegion().rule(";");
            /*
            foreach (Token item in parser.new_queue)
            {
                Console.WriteLine(item.GetValue());
            }*/
            //if (parser.is_matched) Console.WriteLine("yeal");

            //进行报错提示语法错误
        }
    }

    public class Parser
    {
        public List<Token> queue;
        public List<Token> new_queue = new List<Token>();
        int now_count;
        bool in_region;
        List<string> command = new List<string>();
        List<string> parameter = new List<string>();

        Parser parser;
        public bool is_matched = true; //表示整个语句是否匹配成功
        bool is_or_matched; //表示or语句是否匹配成功
        bool in_for;
        
        public Parser(List<Token> _queue)
        {
            queue = _queue;
        }

        void MatchSuccess()
        {
            new_queue.Add(queue[now_count]);
            now_count++;
            is_matched = true;
        }
        void AddCommand(string c, string s)
        {
            if (in_region)
            {
                command.Add(c);
                parameter.Add(s);
            }
        }

        public Parser StartRegion()
        {
            in_region = true;
            return this;
        }
        
        //这个函数会生成一个和现在这个数值一致的parser实例，并把先前记录的 匹配动作和参数 循环执行，直到没有新的匹配，循环结束，返回带有匹配结果的parser对象
        public Parser EndRegion()
        {
            parser = new Parser(queue)
            {
                now_count = now_count,
                new_queue = new_queue,
                is_matched = is_matched,
                is_or_matched = is_or_matched,
                in_for = true
            };
            Parser _parser = new Parser(parser.queue)
            {
                now_count = parser.now_count,
                new_queue = parser.new_queue,
                is_matched = parser.is_matched,
                is_or_matched = parser.is_or_matched,
                in_for = true
            };
            while (true)
            {
                if (!parser.is_matched)
                {
                    _parser.in_for = false;
                    return _parser;
                }
                _parser = new Parser(parser.queue)
                {
                    now_count = parser.now_count,
                    new_queue = parser.new_queue,
                    is_matched = parser.is_matched,
                    is_or_matched = parser.is_or_matched,
                    in_for = true
                };
                for (int i = 0; i < command.Count; i++)
                {
                    switch (command[i])
                    {
                        case "rule":
                            {
                                parser = parser.rule(parameter[i]);
                                break;
                            }
                        case "or":
                            {
                                parser = parser.or(parameter[i]);
                                break;
                            }
                    }
                }
            }
        }
        public Parser rule(string s)
        {
            is_or_matched = false;
            if (!is_matched) return this;
            if (queue.Count == now_count) return this;
            AddCommand("rule", s);
            if (queue[now_count].GetValue() == ";" && s != ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetValue() == s)
            {
                MatchSuccess();
                return this;
            }
            else if (queue[now_count].GetTokenType().ToString() == s && in_for)
            {
                MatchSuccess();
                return this;
            }
            is_matched = false;
            return this;
        }
        public Parser rule(Types t)
        {
            is_or_matched = false;
            if (!is_matched) return this;
            if (queue.Count == now_count) return this;
            AddCommand("rule", t.ToString());
            if (queue[now_count].GetValue() == ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetTokenType().ToString() == t.ToString())
            {
                MatchSuccess();
                return this;
            }
            is_matched = false;
            return this;
        }
        public Parser or(string s)
        {
            //Console.WriteLine(is_matched);
            AddCommand("or", s);
            if (is_or_matched) return this;
            is_matched = false;
            if (queue[now_count].GetValue() == ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetValue() == s)
            {
                MatchSuccess();
                is_or_matched = true;
                return this;
            }
            is_matched = false;
            return this;
        }
        public Parser or(Types t)
        {
            AddCommand("or", t.ToString());
            if (is_or_matched) return this;
            is_matched = false;
            if (queue[now_count].GetValue() == ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetTokenType().ToString() == t.ToString())
            {
                MatchSuccess();
                is_or_matched = true;
                return this;
            }
            is_matched = false;
            return this;
        }
    }
}