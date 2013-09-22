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
    /// Classe de Persistência da entidade Usuario
    /// </summary>
    public class UsuarioDao : Crud<Usuario>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public UsuarioDao()
        {

        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Usuario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Usuario> Listar()
        {
            List<Usuario> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Usuario
        /// </summary>
        /// <param name="id">chave primária da entidade Usuario.</param>
        /// <returns>Retorna uma entidade</returns>
        public Usuario Consultar(int id)
        {
            Usuario entidade = Contexto.Consultar(a => a.IdUsuario == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Usuario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Usuario Incluir(Usuario entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Usuario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Usuario entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Usuario.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Usuario entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<Usuario> ListarAtivos()
        {

            List<Usuario> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Usuario
        /// </summary>
        /// <param name="Login">Login da entidade Usuario.</param>
        /// <returns>Retorna uma entidade</returns>
        public Usuario ConsultarLogin(string Login)
        {
            Usuario entidade = Contexto.Consultar(a => a.Login == Login);
            return entidade;
        }

        #endregion
    }
}
