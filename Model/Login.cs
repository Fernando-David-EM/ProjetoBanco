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

        public Login(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }

        public override string GetColumnEqualsValue()
        {
            var names = SplitAndRemoveParenthesis(GetNameOfTableColumns());

            var values = SplitAndRemoveParenthesis(GetValueOfTableProperties());

            string final = "(";

            for (int i = 0; i < names.Count; i++)
            {
                final += $"{names[i]}={values[i]}";
            }

            final += ")";

            return final;
        }

        public override string GetNameOfTableColumns()
        {
            return "(log_usuario,log_senha)";
        }

        public override string GetValueOfTableProperties()
        {
            return $"(\'{Usuario}\',\'{Senha}\')";
        }

        public override string[] GetProperties()
        {
            return new string[]
            {
                Usuario,
                Senha
            };
        }

        public override BaseModel SetPropertiesFromObjectArray(object[] campos)
        {
            Login login = new Login
            {
                Id = (int)campos[0],
                Usuario = (string)campos[1],
                Senha = (string)campos[2]
            };

            return login;
        }
        private List<string> SplitAndRemoveParenthesis(string text)
        {
            return
                text
                .Split(',')
                .Select(x => x.Trim('(', ')'))
                .ToList();
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

        public override string GetPropriedadeDeValidacao()
        {
            return Usuario;
        }
    }
}
