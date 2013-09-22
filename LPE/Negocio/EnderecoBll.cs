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
    /// Classe de negócio da entidade: Endereco
    /// </summary>
    public class EnderecoBll
    {
        #region Campos privados

        EnderecoDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public EnderecoBll()
        {
            persistencia = new EnderecoDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Endereco
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Endereco> Listar()
        {
            List<Endereco> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Endereco
        /// </summary>
        /// <param name="id">chave primária da entidade Endereco.</param>
        /// <returns>Retorna uma entidade</returns>
        public Endereco Consultar(int id)
        {
            Endereco entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Endereco
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Endereco Inserir(Endereco entidade)
        {
            //entidade.UsuarioInclusao =            
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Endereco.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Endereco entidade)
        {
            Endereco entidadeConsulta = this.Consultar(entidade.IdEndereco);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Endereco.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(Endereco entidade)
        {
            entidade.UsuarioAteracao = "Teste";
            entidade.DataAteracao = DateTime.Now;
            entidade.Excluido = true;
            return persistencia.Alterar(entidade);
        }
        
        #endregion

        #region Métodos personalizado

        public List<Endereco> ListarAtivos()
        {
            List<Endereco> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}