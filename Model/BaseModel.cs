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

        public abstract BaseModel SetPropertiesFromObjectArray(object[] campos);

        public abstract string GetNameOfTableColumns();

        public abstract string GetValueOfTableProperties();

        public abstract string GetColumnEqualsValue();

        public abstract string GetPropriedadeDeValidacao();

        public abstract string[] GetProperties();
    }
}
