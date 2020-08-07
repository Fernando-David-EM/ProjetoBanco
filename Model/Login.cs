using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Model
{
    class Login : BaseModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public Login()
        {

        }

        public Login(List<object> propriedades) : base(propriedades)
        {

        }

        protected override void InserePropriedades(List<object> propriedades)
        {
            if (propriedades.Count > 5)
            {
                Id = Convert.ToInt32(propriedades.First());
                propriedades.RemoveAt(0);
            }
            Usuario = Convert.ToString(propriedades[0]);
            Senha = Convert.ToString(propriedades[1]);
        }

        public override string RecebeColunasIgualValorParaSql()
        {
            var names = RemoveParentesis(RecebeNomeDasColunasDaTabelaParaSql());

            var values = RemoveParentesis(RecebeValorDasPropriedadesParaSql());

            string final = "(";

            for (int i = 0; i < names.Count; i++)
            {
                final += $"{names[i]}={values[i]}";
            }

            final += ")";

            return final;
        }

        public override string RecebeNomeDasColunasDaTabelaParaSql()
        {
            return "(log_usuario,log_senha)";
        }

        public override string RecebeValorDasPropriedadesParaSql()
        {
            return $"(\'{Usuario}\',\'{Senha}\')";
        }

        public override string[] RecebePropriedades()
        {
            return new string[]
            {
                Usuario,
                Senha
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Login login &&
                   Id == login.Id &&
                   Usuario == login.Usuario &&
                   Senha == login.Senha;
        }

        public override int GetHashCode()
        {
            int hashCode = -1915214571;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Usuario);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Senha);
            return hashCode;
        }

        public override string RecebePropriedadeDeValidacao()
        {
            return Usuario;
        }
    }
}
