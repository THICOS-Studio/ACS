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
        public string var_name;
        public string Operator="";
        public BinaryExper Left, Right;
        public string value="";
#region 构造重载
        public BinaryExper(string v = "", BinaryExper l = null, BinaryExper r = null)
        {
            Left = l;
            Right = r;
            value = v;
        }

        public BinaryExper(string v = "")
        {
            Left = null;
            Right = null;
            Operator = "";
            value = v;
        }

        public BinaryExper(Token t)
        {
            Left = null;
            Right = null;
            Operator = "";
            if (t.type == Types.Number)
            {
                value = t.GetNumber().ToString();
            }
            else if(t.type==Types.Identifier)
            {
                string name = t.GetValue();
                if (VariableList.instance.var_name.Contains(name))
                {
                    var_name = name;
                    value = VariableList.instance.var_data[VariableList.instance.var_name.IndexOf(name)];
                }
                else
                {
                    var_name = name;
                    VariableList.instance.var_name.Add(name);
                    VariableList.instance.var_data.Add("");
                    value = "";
                }
            }

            else
            {
                value = t.GetValue();
                //value = "";
            }
           //  = t.GetValue();
        }

        public BinaryExper()
        {
            Left = null;
            Right = null;
            Operator = "";
            value = "";
        }
#endregion
        public string Calculate()
        {
            if (Left != null)
            {
                Left.Calculate();
            }
            else
            {

            }
            if (Right != null)
            {
                Right.Calculate();
            }
            switch (Operator)
            {
                case "+":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if(IsNumeric(Right.value))
                            {
                                value = (float.Parse(Left.value) + float.Parse(Right.value)).ToString();
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
                case "==":
                    {
                        if (Left.value == Right.value)
                        {
                            value = "true";
                        }
                        else
                        {
                            value = "false";
                        }
                        break;
                    }
                case ">=":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) >= float.Parse(Right.value)) ? "true" : "false";
                                break;
                            }
                        }
                       value = Left.value + Right.value;
                        break;
                    }
                case "<=":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) <= float.Parse(Right.value)) ? "true" : "false";
                                break;
                            }
                        }
                        value = Left.value + Right.value;
                        break;
                    }
                case "<":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) < float.Parse(Right.value)) ? "true" : "false";
                                break;
                            }
                        }
                        value = Left.value + Right.value;
                        break;
                    }
                case ">":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) > float.Parse(Right.value)) ? "true" : "false";
                                break;
                            }
                        }
                        value = Left.value + Right.value;
                        break;
                    }
                case "*":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "")Right.value = "0"; 
                                
                                value = (float.Parse(Left.value) * float.Parse(Right.value)).ToString();
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
                case "%":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) % float.Parse(Right.value)).ToString();
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
                case "/":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) / float.Parse(Right.value)).ToString();
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
                case "-":
                    {
                        if (IsNumeric(Left.value))
                        {
                            if (IsNumeric(Right.value))
                            {
                                if (Left.value == "") Left.value = "0";
                                if (Right.value == "") Right.value = "0";
                                value = (float.Parse(Left.value) - float.Parse(Right.value)).ToString();
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
                case "=":
                    {
                        value = Left.value = Right.value;
                        if (Left.var_name != null)
                        {
                            if (VariableList.instance.var_name.Contains(Left.var_name))
                            {
                                VariableList.instance.var_data[VariableList.instance.var_name.IndexOf(Left.var_name)] = value;
                            }
                        }
                        break;
                    }
                case "":
                    {
                        
                        if(Left!=null)
                        value = Left.value;
                        break;
                    }
            }
            return value;
        }
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }


    }
}
