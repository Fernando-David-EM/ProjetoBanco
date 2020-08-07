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

        public string RecebeColunasIgualValorParaSql()
        {
            var colunas = RemoveParenteses(RecebeNomeDasColunasDaTabelaParaSql());

            var valores = RemoveParenteses(RecebeValorDasPropriedadesParaSql());

            return AdicionaVirgulasEntreColunasEValores(colunas, valores);
        }
        public abstract string RecebeNomeDasColunasDaTabelaParaSql();

        public abstract string RecebeValorDasPropriedadesParaSql();

        protected string AdicionaVirgulasEntreColunasEValores(List<string> colunas, List<string> valores)
        {
            string final = "";

            for (int i = 0; i < colunas.Count; i++)
            {
                final += $"{colunas[i]}={valores[i]},";
            }

            final = final.Trim(',');

            return final;
        }

        public abstract string RecebePropriedadeDeValidacao();

        public abstract string[] RecebePropriedades();

        protected List<string> RemoveParenteses(string text)
        {
            return
                text
                .Split(',')
                .Select(x => x.Trim('(', ')'))
                .ToList();
        }
    }
}
