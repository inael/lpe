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
    /// Classe de Persistência da entidade QuestaoToQuestionario
    /// </summary>
    public class QuestaoToQuestionarioDao : Crud<QuestaoToQuestionario>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public QuestaoToQuestionarioDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: QuestaoToQuestionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<QuestaoToQuestionario> Listar()
        {
            List<QuestaoToQuestionario> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: QuestaoToQuestionario
        /// </summary>
        /// <param name="id">chave primária da entidade QuestaoToQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public QuestaoToQuestionario Consultar(int id)
        {
            QuestaoToQuestionario entidade = Contexto.Consultar(a => a.idQuestaoToQuestionario == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: QuestaoToQuestionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public QuestaoToQuestionario Incluir(QuestaoToQuestionario entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: QuestaoToQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(QuestaoToQuestionario entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: QuestaoToQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(QuestaoToQuestionario entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<QuestaoToQuestionario> ListarAtivos()
        {
            List<QuestaoToQuestionario> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public List<QuestaoToQuestionario> ListarQuestaoToQuestionario (int idQuestionario)
        {
            List<QuestaoToQuestionario> lista = Contexto.Listar(a => a.idQuestionario.IdQuestionario == idQuestionario && a.Excluido == false).ToList();

            return lista;
        }

        #endregion
    }
}
