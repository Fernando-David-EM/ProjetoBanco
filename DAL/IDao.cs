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
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
        T GetByID(int id);
    }
}