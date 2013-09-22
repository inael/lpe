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

#endregion

namespace Negocio
{
    /// <summary>
    /// Classe de negócio da entidade: GraficoResposta
    /// </summary>
    public class GraficoRespostaBll
    {
        #region Campos privados

        GraficoRespostaDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public GraficoRespostaBll()
        {
            persistencia = new GraficoRespostaDao();
        }

        #endregion

        #region Métodos personalizados

        public IList<GraficoResposta> getChart(string Sql)
        {
            IList<GraficoResposta> lista = persistencia.getChart(Sql);
            return lista;
        }

        #endregion
    }
}