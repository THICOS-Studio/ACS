using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_Lexer
{
    public class ASLeaf : ASTree
    {
        new ASTree left;
        new ASTree right;
        new Token Operator;
        public Token value;
        public string name;
    }
}
