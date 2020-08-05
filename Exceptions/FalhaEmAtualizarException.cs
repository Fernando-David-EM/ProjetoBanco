﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Exceptions
{
    class FalhaEmAtualizarException : Exception
    {
        public FalhaEmAtualizarException()
        {
        }

        public FalhaEmAtualizarException(string message)
            : base(message)
        {
        }

        public FalhaEmAtualizarException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}