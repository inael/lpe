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
    /// Classe de negócio da entidade: QuestaoGrupo
    /// </summary>
    public class QuestaoGrupoBll
    {
        #region Campos privados

        QuestaoGrupoDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public QuestaoGrupoBll()
        {
            persistencia = new QuestaoGrupoDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: QuestaoGrupo
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<QuestaoGrupo> Listar()
        {
            List<QuestaoGrupo> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: QuestaoGrupo
        /// </summary>
        /// <param name="id">chave primária da entidade QuestaoGrupo.</param>
        /// <returns>Retorna uma entidade</returns>
        public QuestaoGrupo Consultar(int id)
        {
            QuestaoGrupo entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: QuestaoGrupo
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public QuestaoGrupo Inserir(QuestaoGrupo entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioQuestaoGrupo.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeQuestaoGrupo.Id);

            entidade.CidadeQuestaoGrupo = entidadeCidades;
            entidade.RamoNegocioQuestaoGrupo = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: QuestaoGrupo.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(QuestaoGrupo entidade)
        {
            QuestaoGrupo entidadeConsulta = this.Consultar(entidade.IdGrupo);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        public List<QuestaoGrupo> ListarAtivos()
        {
            List<QuestaoGrupo> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}