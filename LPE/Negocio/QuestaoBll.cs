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
    /// Classe de negócio da entidade: Questao
    /// </summary>
    public class QuestaoBll
    {
        #region Campos privados

        QuestaoDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public QuestaoBll()
        {
            persistencia = new QuestaoDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Questao
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Questao> Listar()
        {
            List<Questao> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Questao
        /// </summary>
        /// <param name="id">chave primária da entidade Questao.</param>
        /// <returns>Retorna uma entidade</returns>
        public Questao Consultar(int id)
        {
            Questao entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Questao
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Questao Inserir(Questao entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioQuestao.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeQuestao.Id);

            entidade.CidadeQuestao = entidadeCidades;
            entidade.RamoNegocioQuestao = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Questao.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Questao entidade)
        {
            Questao entidadeConsulta = this.Consultar(entidade.IdQuestao);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        public List<Questao> ListarAtivos()
        {
            List<Questao> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}