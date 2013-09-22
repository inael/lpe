/*
 * Classe de negócio
 * Arquiteto: José Lino Neto
 * Desenvolvedor: 
 * 
 */

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using Persistencia;
using AutoCompleteModel;

#endregion

namespace Negocio
{
    /// <summary>
    /// Classe de negócio da entidade: AutoCompleteResult
    /// </summary>
    public class AutoCompleteResultBll
    {
        #region Campos privados

        AutoCompleteResultDao<AutoCompleteItem> persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public AutoCompleteResultBll()
        {
            persistencia = new AutoCompleteResultDao<AutoCompleteItem>();
        }

        #endregion

        #region Métodos

        /*public IList<AutoCompleteItem> GetAutoCompleteList(string Sql)
        {
            IList<AutoCompleteItem> lista = persistencia.GetAutoCompleteList(Sql);
            return lista;
        }*/

        #endregion
    }
}