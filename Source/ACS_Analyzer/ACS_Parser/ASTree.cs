using System;
using System.Collections;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_Lexer
{
    public abstract class ASTree
    {
        public ASTree left;
        public ASTree right;
        public Token Operator;
    }
}
