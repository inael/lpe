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
    /// Classe de Persistência da entidade OpcaoRespostaToQuestionario
    /// </summary>
    public class OpcaoRespostaToQuestionarioDao : Crud<OpcaoRespostaToQuestionario>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public OpcaoRespostaToQuestionarioDao()
        {
        }

        #endregion

        #region Propriedades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: OpcaoRespostaToQuestionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<OpcaoRespostaToQuestionario> Listar()
        {
            List<OpcaoRespostaToQuestionario> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: OpcaoRespostaToQuestionario
        /// </summary>
        /// <param name="id">chave primária da entidade OpcaoRespostaToQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public OpcaoRespostaToQuestionario Consultar(int id)
        {
            OpcaoRespostaToQuestionario entidade = Contexto.Consultar(a => a.IdOpcaoRespostaQuestionario == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: OpcaoRespostaToQuestionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public OpcaoRespostaToQuestionario Incluir(OpcaoRespostaToQuestionario entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: OpcaoRespostaToQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(OpcaoRespostaToQuestionario entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: OpcaoRespostaToQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(OpcaoRespostaToQuestionario entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<OpcaoRespostaToQuestionario> ListarAtivos()
        {
            List<OpcaoRespostaToQuestionario> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public List<OpcaoRespostaToQuestionario> ListarOpcaoRespostaToQuestionario(int idQuestionario)
        {
            List<OpcaoRespostaToQuestionario> lista = Contexto.Listar(a => a.idQuestionario.IdQuestionario == idQuestionario && a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
