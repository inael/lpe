/*
 * Classe de negócio
 * Arquiteto: José Lino Neto
 * Desenvolvedor: 
 * 
 */

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using Persistencia;

#endregion

namespace Negocio
{
    /// <summary>
    /// Classe de negócio da entidade: Pessoa
    /// </summary>
    public class PessoaBll
    {
        #region Campos privados

        PessoaDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public PessoaBll()
        {
            persistencia = new PessoaDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Pessoa
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Pessoa> Listar()
        {
            List<Pessoa> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Pessoa
        /// </summary>
        /// <param name="id">chave primária da entidade Pessoa.</param>
        /// <returns>Retorna uma entidade</returns>
        public Pessoa Consultar(int id)
        {
            Pessoa entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Pessoa
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Pessoa Inserir(Pessoa entidade)
        {
            //entidade.UsuarioInclusao =            
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Pessoa.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Pessoa entidade)
        {
            Pessoa entidadeConsulta = this.Consultar(entidade.IdPessoa);
            entidade.UsuarioAteracao = entidadeConsulta.NomePessoa;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Pessoa.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(Pessoa entidade)
        {
            /*Pessoa entidadeConsulta = this.Consultar(entidade.IdPessoa);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;*/
            entidade.UsuarioAteracao = "Teste";
            entidade.DataAteracao = DateTime.Now;
            entidade.Excluido = true;
            return persistencia.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizado

        public List<Pessoa> ListarAtivos()
        {
            List<Pessoa> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}