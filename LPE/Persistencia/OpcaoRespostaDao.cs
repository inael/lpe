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
    /// Classe de Persistência da entidade OpcaoResposta
    /// </summary>
    public class OpcaoRespostaDao : Crud<OpcaoResposta>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public OpcaoRespostaDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: OpcaoResposta
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<OpcaoResposta> Listar()
        {
            List<OpcaoResposta> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: OpcaoResposta
        /// </summary>
        /// <param name="id">chave primária da entidade OpcaoResposta.</param>
        /// <returns>Retorna uma entidade</returns>
        public OpcaoResposta Consultar(int id)
        {
            OpcaoResposta entidade = Contexto.Consultar(a => a.IdOpcaoResposta == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: OpcaoResposta
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public OpcaoResposta Incluir(OpcaoResposta entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: OpcaoResposta.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(OpcaoResposta entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: OpcaoResposta.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(OpcaoResposta entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

            public List<OpcaoResposta> ListarAtivos()
            {
                List<OpcaoResposta> lista = Contexto.Listar(a => a.Excluido == false).ToList();
                return lista;
            }

        #endregion
    }
}
