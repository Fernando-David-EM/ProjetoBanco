using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class TelefoneInvalidoException : Exception
    {
        public TelefoneInvalidoException()
        {
        }

        public TelefoneInvalidoException(string message)
            : base("Telefone inválido!\n" + message)
        {
        }

        public TelefoneInvalidoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
