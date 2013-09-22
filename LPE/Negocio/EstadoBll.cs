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
using AutoCompleteModel;

#endregion

namespace Negocio
{
    /// <summary>
    /// Classe de negócio da entidade: Estado
    /// </summary>
    public class EstadoBll
    {
        #region Campos privados

        EstadoDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public EstadoBll()
        {
            persistencia = new EstadoDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Estado
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Estado> Listar()
        {
            List<Estado> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Estado
        /// </summary>
        /// <param name="id">chave primária da entidade Estado.</param>
        /// <returns>Retorna uma entidade</returns>
        public Estado Consultar(int id)
        {
            Estado entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Estado
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Estado Inserir(Estado entidade)
        {
            //entidade.UsuarioInclusao =            
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Estado.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Estado entidade)
        {
            Estado entidadeConsulta = this.Consultar(entidade.IdEstado);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Estado.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(Estado entidade)
        {
            /*Estado entidadeConsulta = this.Consultar(entidade.IdEstado);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;*/
            entidade.UsuarioAteracao = "Teste";
            entidade.DataAteracao = DateTime.Now;
            entidade.Excluido = true;
            return persistencia.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizado

        public List<Estado> ListarAtivos()
        {
            List<Estado> lista = persistencia.ListarAtivos();
            return lista;
        }

        public IList<Estado> ListarMenuUsr(string Sql)
        {
            IList<Estado> lista = persistencia.ListarMenuUsr(Sql);
            return lista;
        }

        #endregion
    }
}