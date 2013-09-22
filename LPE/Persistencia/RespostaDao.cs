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
    /// Classe de Persistência da entidade Resposta
    /// </summary>
    public class RespostaDao : Crud<Resposta>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public RespostaDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Resposta
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Resposta> Listar()
        {
            List<Resposta> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Resposta
        /// </summary>
        /// <param name="id">chave primária da entidade Resposta.</param>
        /// <returns>Retorna uma entidade</returns>
        public Resposta Consultar(int id)
        {
            Resposta entidade = Contexto.Consultar(a => a.IdResposta == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Resposta
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Resposta Incluir(Resposta entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Resposta.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Resposta entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Resposta.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Resposta entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion
     }
}
