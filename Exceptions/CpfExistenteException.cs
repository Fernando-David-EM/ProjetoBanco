using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class CpfExistenteException : Exception
    {
        public CpfExistenteException()
        {
        }

        public CpfExistenteException(string message)
            : base("Cpf existente!\n" + message)
        {
        }

        public CpfExistenteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
