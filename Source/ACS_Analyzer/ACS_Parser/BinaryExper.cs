using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS_Lexer;
using System.Text.RegularExpressions;

namespace ACS_Analyzer.ACS_Parser
{
    class BinaryExper:Token
    {
        public BinaryExper top;
        public string Operator;
        public BinaryExper Left, Right;
        public string value;

        public BinaryExper(string v = "", BinaryExper l = null, BinaryExper r = null)
        {
            Left = l;
            Right = r;
            value = v;
        }

        public void Calculate()
        {
            switch (Operator)
            {
                case "+":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if(IsNumeric(Right.value))
                            {
                                value = (int.Parse(Left.value) + int.Parse(Right.value)).ToString();
                            }
                            else
                            {
                                value = Left.value + Right.value;
                            }
                        }
                        else
                        {
                            value = Left.value + Right.value;
                        }
                        break;
                    }
            }
        }
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
    }
}
