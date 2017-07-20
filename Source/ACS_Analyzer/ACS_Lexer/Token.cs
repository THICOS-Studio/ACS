﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_Lexer
{
    public enum Types
    {
        Identifier,
        Number,
        String,
        Float
    }

    public abstract class Token
    {
        public static readonly Token EOF = null;
        public static string EOL = "\\n";
        public Types type;

        public string text;
        public int int_value;
        public float float_value;

        public virtual int GetNumber()
        {
            throw new Exception("not number token");
        }
        public virtual float GetFloat()
        {
            throw new Exception("not float token");
        }
        public virtual string GetText()
        {
            return "";
        }
        public Types GetTokenType()
        {
            return type;
        }
        public string GetValue()
        {
            switch (type)
            {
                case Types.Float:
                    {
                        return float_value.ToString();
                    }
                case Types.Identifier:
                    {
                        return text;
                    }
                case Types.Number:
                    {
                        return int_value.ToString();
                    }
                case Types.String:
                    {
                        return text;
                    }
            }
            return "";
        }
    }

    #region DefinitionTypes
    class IdentifierToken : Token
    {

        public IdentifierToken(string id)
        {
            type = Types.Identifier;
            text = id;
        }
        public override string GetText()
        {
            return text;
        }
    }
    class NumberToken : Token
    {

        public NumberToken(int v)
        {
            type = Types.Number;
            int_value = v;
        }
        public override int GetNumber()
        {
            return int_value;
        }
    }
    class StringToken : Token
    {

        public StringToken(string str)
        {
            type = Types.String;
            text = str;
        }
        public override string GetText()
        {
            return text;
        }
    }
    class FloatToken : Token
    {
        public FloatToken(float v)
        {
            type = Types.Float;
            float_value = v;
        }
        public override float GetFloat()
        {
            return float_value;
        }
    }
    #endregion
}
