using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class PesquisaSemSucessoException : Exception
    {
        public PesquisaSemSucessoException()
        {
        }

        public PesquisaSemSucessoException(string message)
            : base("Falha no sql de pesquisa!\nTabela : " + message)
        {
        }

        public PesquisaSemSucessoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
