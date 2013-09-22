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
using AutoCompleteModel;

#endregion

namespace Persistencia
{
    /// <summary>
    /// Classe de Persistência da entidade Estado
    /// </summary>
    public class EstadoDao : Crud<Estado> 
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public EstadoDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Estado
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Estado> Listar()
        {
            List<Estado> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Estado
        /// </summary>
        /// <param name="id">chave primária da entidade Estado.</param>
        /// <returns>Retorna uma entidade</returns>
        public Estado Consultar(int id)
        {
            Estado entidade = Contexto.Consultar(a => a.IdEstado == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Estado
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Estado Incluir(Estado entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Estado.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Estado entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Estado.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Estado entidade)
        {
            return Contexto.Excluir(entidade);
        }

        /// <summary>
        /// Método para exclusão lógica de entidade do tipo: Estado.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a exclusão.</returns>
        public bool ExcluirLogico(Estado entidade)
        {
            return Contexto.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<Estado> ListarAtivos()
        {
            List<Estado> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public IList<Estado> ListarMenuUsr(string Sql)
        {
            IList<Estado> lista = Contexto.ExecutarSqlListagem<Estado>(Sql);
            return lista;
        }

        #endregion
    }
}
