using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class FalhaEmInserirException : Exception
    {
        public FalhaEmInserirException()
        {
        }

        public FalhaEmInserirException(string message)
            : base("Falha no sql de inserção!\nTabela : " + message)
        {
        }

        public FalhaEmInserirException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
