using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using AutoCompleteModel;

namespace Persistencia
{
    public class AutoCompleteResultDao<T> : Crud<T> where T : class
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public AutoCompleteResultDao()
        {

        }

        #endregion

        public List<T> GetAutoCompleteList<T>(string Sql) where T : new()
        {
           List<T> lista = Contexto.ExecutarSqlListagem<T>(Sql);
           return lista;
        }
    }
}
