using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Model
{
    abstract class BaseModel
    {
        public int Id { get; set; }

        public abstract BaseModel RecebeContaComPropriedadesDeCampos(object[] campos);

        public abstract string RecebeNomeDasColunasDaTabelaParaSql();

        public abstract string RecebeValorDasPropriedadesParaSql();

        public abstract string RecebeColunasIgualValorParaSql();

        public abstract string RecebePropriedadeDeValidacao();

        public abstract string[] RecebePropriedades();
    }
}
