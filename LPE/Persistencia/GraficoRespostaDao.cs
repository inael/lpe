/*
 * Classe de persistência
 * Arquiteto: José Lino Neto
 * Desenvolvedor: 
 * 
 */

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using Modelo;

#endregion

namespace Persistencia
{
    /// <summary>
    /// Classe de Persistência da entidade GraficoResposta
    /// </summary>
    public class GraficoRespostaDao : Crud<GraficoResposta>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public GraficoRespostaDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos personalizados

        public IList<GraficoResposta> getChart(string Sql)
        {
            IList<GraficoResposta> lista = Contexto.ExecutarSqlListagem<GraficoResposta>(Sql);

            return lista;
        }

        #endregion
    }
}
