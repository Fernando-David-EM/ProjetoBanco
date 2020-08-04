using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class FalhaEmDeletarException : Exception
    {
        public FalhaEmDeletarException()
        {
        }

        public FalhaEmDeletarException(string message)
            : base(message)
        {
        }

        public FalhaEmDeletarException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
