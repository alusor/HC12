using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC12
{
    class Instruction
    {
        public string etiqueta { get; set; }
        public string codop { get; set; }
        public string operando { get; set; }

        public Instruction() {
            operando = codop = etiqueta = "NULL";
        }
    }
}
