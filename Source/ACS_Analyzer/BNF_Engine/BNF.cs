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
                if (item.GetValue() == ";") break;
                input.Add(item);
            }
            //只取了一句进行测试
            Parser parser = new Parser();
            parser.SetInput(input);
            
            parser = parser.rule(Types.Identifier).rule(Types.Operator).rule(Types.Number);
            Console.WriteLine("yeal");
        }
    }

    public class Parser
    {
        List<Token> queue;
        public List<Token> new_queue = new List<Token>();
        int now_count;
        Parser parser;
        public ASTree _ASTree;

        public void SetInput(List<Token> _queue)
        {
            queue = _queue;
        }

        public Parser rule(string s)
        {
            if (queue[now_count].GetValue() == ";")
            {
                return new Parser();
            }
            if (queue[now_count].GetValue() == s)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                return this;
            }
            return new Parser();
        }
        public Parser rule(Types t)
        {
            if (queue[now_count].GetValue() == ";")
            {
                return new Parser();
            }
            if (queue[now_count].GetTokenType() == t)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                return this;
            }
            return new Parser();
        }
        public Parser or(string s)
        {
            if (queue[now_count].GetValue() == ";")
            {
                return new Parser();
            }
            if (queue[now_count].GetValue() == s)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                return this;
            }
            return new Parser();
        }
        public Parser or(Types t)
        {
            now_count--;
            if (queue[now_count].GetValue() == ";")
            {
                return new Parser();
            }
            if (queue[now_count].GetTokenType() == t)
            {
                new_queue.Add(queue[now_count]);
                now_count++;
                return this;
            }
            return new Parser();
        }
    }
}
