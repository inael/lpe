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
    /// Classe de negócio da entidade: QuestaoToQuestionario
    /// </summary>
    public class QuestaoToQuestionarioBll
    {
        #region Campos privados

        QuestaoToQuestionarioDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public QuestaoToQuestionarioBll()
        {
            persistencia = new QuestaoToQuestionarioDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: QuestaoToQuestionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<QuestaoToQuestionario> Listar()
        {
            List<QuestaoToQuestionario> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: QuestaoToQuestionario
        /// </summary>
        /// <param name="id">chave primária da entidade QuestaoToQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public QuestaoToQuestionario Consultar(int id)
        {
            QuestaoToQuestionario entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: QuestaoToQuestionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public QuestaoToQuestionario Inserir(QuestaoToQuestionario entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioQuestaoToQuestionario.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeQuestaoToQuestionario.Id);

            entidade.CidadeQuestaoToQuestionario = entidadeCidades;
            entidade.RamoNegocioQuestaoToQuestionario = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: QuestaoToQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(QuestaoToQuestionario entidade)
        {
            QuestaoToQuestionario entidadeConsulta = this.Consultar(entidade.idQuestaoToQuestionario);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

    #endregion
        
        #region Métodos personalizados

            public List<QuestaoToQuestionario> ListarAtivos()
            {
                List<QuestaoToQuestionario> lista = persistencia.ListarAtivos();
                return lista;
            }

            public List<QuestaoToQuestionario> ListarQuestaoToQuestionario(int idQuestionario)
            {
                List<QuestaoToQuestionario> lista = persistencia.ListarQuestaoToQuestionario(idQuestionario);
                return lista;
            }

        #endregion
    }
}