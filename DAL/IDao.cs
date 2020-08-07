using Banco.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.DAL
{
    interface IDao<T> where T : BaseModel
    {
        void Insere(T item);
        void Atualiza(T item, bool mudouPropriedadeDeValidacao);
        void Deleta(T item);
        List<T> PesquisaTodos();
        T PesquisaPorId(int id);
    }
}