using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS_Analyzer.ACS_Parser
{
    class VariableList
    {
        public static VariableList instance;
        public List<string> var_name = new List<string>();
        public List<string> var_data = new List<string>();
        public VariableList()
        {
            instance = this;
        }
      
    }
}

