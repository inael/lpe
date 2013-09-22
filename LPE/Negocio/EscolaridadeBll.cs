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
    /// Classe de negócio da entidade: Escolaridade
    /// </summary>
    public class EscolaridadeBll
    {
        #region Campos privados

        EscolaridadeDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public EscolaridadeBll()
        {
            persistencia = new EscolaridadeDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Escolaridade
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Escolaridade> Listar()
        {
            List<Escolaridade> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Escolaridade
        /// </summary>
        /// <param name="id">chave primária da entidade Escolaridade.</param>
        /// <returns>Retorna uma entidade</returns>
        public Escolaridade Consultar(int id)
        {
            Escolaridade entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Escolaridade
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Escolaridade Inserir(Escolaridade entidade)
        {
            //entidade.UsuarioInclusao =            
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Escolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Escolaridade entidade)
        {
            Escolaridade entidadeConsulta = this.Consultar(entidade.IdEscolaridade);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Escolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(Escolaridade entidade)
        {
            /*Escolaridade entidadeConsulta = this.Consultar(entidade.IdEscolaridade);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;*/
            entidade.UsuarioAteracao = "Teste";
            entidade.DataAteracao = DateTime.Now;
            entidade.Excluido = true;
            return persistencia.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizado

        public List<Escolaridade> ListarAtivos()
        {
            List<Escolaridade> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}