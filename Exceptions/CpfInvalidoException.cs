using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class CpfInvalidoException : Exception
    {
        public CpfInvalidoException()
        {
        }

        public CpfInvalidoException(string message)
            : base(message)
        {
        }

        public CpfInvalidoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
