using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Management_System
{
    public class Expression
    {
        public Operation Action { get; set; }
        public string FirstToken { get; set; }
        public string SecondToken { get; set; }

    }
}
