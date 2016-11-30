using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{
    class InterpreterException : Exception
    {
        public InterpreterException()
            : base("Interpreter error")
        {
        }
    }
}
