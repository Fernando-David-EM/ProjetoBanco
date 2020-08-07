using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Model
{
    abstract class BaseModel
    {
        public BaseModel(List<object> propriedades)
        {
            InserePropriedades(propriedades);
        }

        public BaseModel() 
        { 
        
        }

        protected abstract void InserePropriedades(List<object> propriedades);

        public int Id { get; set; }

        public abstract string RecebeNomeDasColunasDaTabelaParaSql();

        public abstract string RecebeValorDasPropriedadesParaSql();

        public abstract string RecebeColunasIgualValorParaSql();

        public abstract string RecebePropriedadeDeValidacao();

        public abstract string[] RecebePropriedades();
        protected List<string> RemoveParentesis(string text)
        {
            return
                text
                .Split(',')
                .Select(x => x.Trim('(', ')'))
                .ToList();
        }
    }
}
