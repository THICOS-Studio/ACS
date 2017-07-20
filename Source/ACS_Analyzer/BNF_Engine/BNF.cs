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
            Parser parser = new Parser();
            parser.SetInput(_input);
            parser = parser.rule(Types.Identifier).rule(Types.Operator).rule(Types.Number).rule(";");
            if (parser.is_matched) Console.WriteLine("yeal");
        }
    }

    public class Parser
    {
        public List<Token> queue;
        public List<Token> new_queue = new List<Token>();
        int now_count;
        public ASTree _ASTree;
        public bool is_matched = true;

        public void SetInput(List<Token> _queue)
        {
            queue = _queue;
        }

        public Parser rule(string s)
        {
            if (!is_matched) return this;
            if (queue[now_count].GetValue() == ";" && s != ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetValue() == s)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                is_matched = true;
                return this;
            }
            is_matched = false;
            return this;
        }
        public Parser rule(Types t)
        {
            if (!is_matched) return this;
            if (queue[now_count].GetValue() == ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetTokenType() == t)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                is_matched = true;
                return this;
            }
            is_matched = false;
            return this;
        }
        public Parser or(string s)
        {
            if (!is_matched) return this;
            now_count--;
            if (queue[now_count].GetValue() == ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetValue() == s)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                is_matched = true;
                return this;
            }
            is_matched = false;
            return this;
        }
        public Parser or(Types t)
        {
            if (!is_matched) return this;
            now_count--;
            if (queue[now_count].GetValue() == ";")
            {
                is_matched = false;
                return this;
            }
            if (queue[now_count].GetTokenType() == t)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                is_matched = true;
                return this;
            }
            is_matched = false;
            return this;
        }
    }
}
