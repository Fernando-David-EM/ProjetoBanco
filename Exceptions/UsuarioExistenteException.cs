using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class UsuarioExistenteException : Exception
    {
        public UsuarioExistenteException()
        {
        }

        public UsuarioExistenteException(string message)
            : base("Usuário já existe!\n" + message)
        {
        }

        public UsuarioExistenteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
