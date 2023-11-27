using NovoTayUmDoce.Conexão;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{

    /// <summary>
    ///     Abstract Class para classes DAO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class AbstractDAO<T>
    {
        protected Conexao conn = new Conexao();

        public virtual void Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
