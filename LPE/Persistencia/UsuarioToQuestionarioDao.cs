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
    /// Classe de Persistência da entidade UsuarioQuestionario
    /// </summary>
    public class UsuarioToQuestionarioDao : Crud<UsuarioToQuestionario>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public UsuarioToQuestionarioDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: UsuarioQuestionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<UsuarioToQuestionario> Listar()
        {
            List<UsuarioToQuestionario> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: UsuarioQuestionario
        /// </summary>
        /// <param name="id">chave primária da entidade UsuarioQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public UsuarioToQuestionario Consultar(int id)
        {
            UsuarioToQuestionario entidade = Contexto.Consultar(a => a.IdUsuarioQuestionario == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: UsuarioQuestionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public UsuarioToQuestionario Incluir(UsuarioToQuestionario entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: UsuarioQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(UsuarioToQuestionario entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: UsuarioQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(UsuarioToQuestionario entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<UsuarioToQuestionario> ListarAtivos()
        {
            List<UsuarioToQuestionario> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public List<UsuarioToQuestionario> GetUserQuestionario(string Login) 
        {
            List<UsuarioToQuestionario> lista = Contexto.Listar(a => a.idUsuario.Login == Login).ToList();
            return lista;
        }

        public List<UsuarioToQuestionario> isNotQuestComplete()
        {
            List<UsuarioToQuestionario> lista = Contexto.Listar(a => a.Concluido == false && a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
