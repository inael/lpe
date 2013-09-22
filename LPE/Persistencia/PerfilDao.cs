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
    /// Classe de Persistência da entidade Perfil
    /// </summary>
    public class PerfilDao : Crud<Perfil>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public PerfilDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Perfil
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Perfil> Listar()
        {
            List<Perfil> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Perfil
        /// </summary>
        /// <param name="id">chave primária da entidade Perfil.</param>
        /// <returns>Retorna uma entidade</returns>
        public Perfil Consultar(int id)
        {
            Perfil entidade = Contexto.Consultar(a => a.IdPerfil == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Perfil
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Perfil Incluir(Perfil entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Perfil.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Perfil entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Perfil.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Perfil entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<Perfil> ListarAtivos()
        {
            List<Perfil> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
