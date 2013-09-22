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
    /// Classe de Persistência da entidade NivelEscolaridade
    /// </summary>
    public class NivelEscolaridadeDao : Crud<NivelEscolaridade>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public NivelEscolaridadeDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: NivelEscolaridade
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<NivelEscolaridade> Listar()
        {
            List<NivelEscolaridade> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: NivelEscolaridade
        /// </summary>
        /// <param name="id">chave primária da entidade NivelEscolaridade.</param>
        /// <returns>Retorna uma entidade</returns>
        public NivelEscolaridade Consultar(int id)
        {
            NivelEscolaridade entidade = Contexto.Consultar(a => a.IdNivelEscolaridade == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: NivelEscolaridade
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public NivelEscolaridade Incluir(NivelEscolaridade entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: NivelEscolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(NivelEscolaridade entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: NivelEscolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(NivelEscolaridade entidade)
        {
            return Contexto.Excluir(entidade);
        }

        /// <summary>
        /// Método para exclusão lógica de entidade do tipo: NivelEscolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a exclusão.</returns>
        public bool ExcluirLogico(NivelEscolaridade entidade)
        {
            return Contexto.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<NivelEscolaridade> ListarAtivos()
        {
            List<NivelEscolaridade> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
