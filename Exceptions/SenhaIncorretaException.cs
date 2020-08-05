using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class SenhaIncorretaException : Exception
    {
        public SenhaIncorretaException()
        {
        }

        public SenhaIncorretaException(string message)
            : base(message)
        {
        }

        public SenhaIncorretaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
