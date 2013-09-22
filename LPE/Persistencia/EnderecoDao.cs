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
    /// Classe de Persistência da entidade Endereco
    /// </summary>
    public class EnderecoDao : Crud<Endereco>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public EnderecoDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Endereco
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Endereco> Listar()
        {
            List<Endereco> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Endereco
        /// </summary>
        /// <param name="id">chave primária da entidade Endereco.</param>
        /// <returns>Retorna uma entidade</returns>
        public Endereco Consultar(int id)
        {
            Endereco entidade = Contexto.Consultar(a => a.IdEndereco == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Endereco
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Endereco Incluir(Endereco entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Endereco.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Endereco entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Endereco.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Endereco entidade)
        {
            return Contexto.Excluir(entidade);
        }

        /// <summary>
        /// Método para exclusão lógica de entidade do tipo: Endereco.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a exclusão.</returns>
        public bool ExcluirLogico(Endereco entidade)
        {
            return Contexto.Alterar(entidade);
        }  
        
        #endregion

        #region Métodos personalizados

        public List<Endereco> ListarAtivos()
        {
            List<Endereco> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
