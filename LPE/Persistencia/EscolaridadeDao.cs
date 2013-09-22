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
    /// Classe de Persistência da entidade Escolaridade
    /// </summary>
    public class EscolaridadeDao : Crud<Escolaridade>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public EscolaridadeDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Escolaridade
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Escolaridade> Listar()
        {
            List<Escolaridade> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Escolaridade
        /// </summary>
        /// <param name="id">chave primária da entidade Escolaridade.</param>
        /// <returns>Retorna uma entidade</returns>
        public Escolaridade Consultar(int id)
        {
            Escolaridade entidade = Contexto.Consultar(a => a.IdEscolaridade == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Escolaridade
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Escolaridade Incluir(Escolaridade entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Escolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Escolaridade entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Escolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Escolaridade entidade)
        {
            return Contexto.Excluir(entidade);
        }

        /// <summary>
        /// Método para exclusão lógica de entidade do tipo: Escolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a exclusão.</returns>
        public bool ExcluirLogico(Escolaridade entidade)
        {
            return Contexto.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<Escolaridade> ListarAtivos()
        {
            List<Escolaridade> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
