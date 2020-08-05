using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class UsuarioInexistenteException : Exception
    {
        public UsuarioInexistenteException()
        {
        }

        public UsuarioInexistenteException(string message)
            : base(message)
        {
        }

        public UsuarioInexistenteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
