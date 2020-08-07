using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class CampoNaoPreenchidoException : Exception
    {
        public CampoNaoPreenchidoException()
        {
        }

        public CampoNaoPreenchidoException(string message)
            : base("Campo não preenchido!\n" + message)
        {
        }

        public CampoNaoPreenchidoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
