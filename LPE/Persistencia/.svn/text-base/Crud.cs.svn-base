/*
* Framework de Acesso a Dados utilizando NHibernate
* Arquiteto: José Lino Neto - joselinoneto@gmail.com
*/

#region Using

using Fabrica;
using FabricaNhibernate;
using NHibernate;
using Ninject;

#endregion

namespace Persistencia
{
    public class Crud<T> where T : class
    {
        #region Propriedades

        protected ICrud<T> Contexto { get; private set; }

        #endregion

        #region Construtores

        public Crud()
        {
            StandardKernel niject = new StandardKernel();

            #region NHibernate

            niject.Bind(typeof(ICrud<T>)).To(typeof(ContextoNh<T>));
            niject.Bind(typeof(ISession)).ToConstant(ContextoNh<T>.ConstruirFabrica.OpenSession());
            Contexto = (ContextoNh<T>)niject.Get(typeof(ContextoNh<T>));

            #endregion
        }

        #endregion
    }
}
