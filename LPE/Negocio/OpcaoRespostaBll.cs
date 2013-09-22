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
    /// Classe de negócio da entidade: OpcaoResposta
    /// </summary>
    public class OpcaoRespostaBll
    {
        #region Campos privados

        OpcaoRespostaDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public OpcaoRespostaBll()
        {
            persistencia = new OpcaoRespostaDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: OpcaoResposta
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<OpcaoResposta> Listar()
        {
            List<OpcaoResposta> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: OpcaoResposta
        /// </summary>
        /// <param name="id">chave primária da entidade OpcaoResposta.</param>
        /// <returns>Retorna uma entidade</returns>
        public OpcaoResposta Consultar(int id)
        {
            OpcaoResposta entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: OpcaoResposta
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public OpcaoResposta Inserir(OpcaoResposta entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioOpcaoResposta.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeOpcaoResposta.Id);

            entidade.CidadeOpcaoResposta = entidadeCidades;
            entidade.RamoNegocioOpcaoResposta = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: OpcaoResposta.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(OpcaoResposta entidade)
        {
            OpcaoResposta entidadeConsulta = this.Consultar(entidade.IdOpcaoResposta);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        public List<OpcaoResposta> ListarAtivos()
        {
            List<OpcaoResposta> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}