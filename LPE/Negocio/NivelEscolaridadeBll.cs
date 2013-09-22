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
    /// Classe de negócio da entidade: NivelEscolaridade
    /// </summary>
    public class NivelEscolaridadeBll
    {
        #region Campos privados

        NivelEscolaridadeDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public NivelEscolaridadeBll()
        {
            persistencia = new NivelEscolaridadeDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: NivelEscolaridade
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<NivelEscolaridade> Listar()
        {
            List<NivelEscolaridade> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: NivelEscolaridade
        /// </summary>
        /// <param name="id">chave primária da entidade NivelEscolaridade.</param>
        /// <returns>Retorna uma entidade</returns>
        public NivelEscolaridade Consultar(int id)
        {
            NivelEscolaridade entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: NivelEscolaridade
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public NivelEscolaridade Inserir(NivelEscolaridade entidade)
        {
            //entidade.UsuarioInclusao =            
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: NivelEscolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(NivelEscolaridade entidade)
        {
            NivelEscolaridade entidadeConsulta = this.Consultar(entidade.IdNivelEscolaridade);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: NivelEscolaridade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(NivelEscolaridade entidade)
        {
            entidade.UsuarioAteracao = "Teste";
            entidade.DataAteracao = DateTime.Now;
            entidade.Excluido = true;
            return persistencia.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizado

        public List<NivelEscolaridade> ListarAtivos()
        {
            List<NivelEscolaridade> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}