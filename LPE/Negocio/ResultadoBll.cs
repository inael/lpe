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
    /// Classe de negócio da entidade: Resultado
    /// </summary>
    public class ResultadoBll
    {
        #region Campos privados

        ResultadoDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public ResultadoBll()
        {
            persistencia = new ResultadoDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Resultado
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Resultado> Listar()
        {
            List<Resultado> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Resultado
        /// </summary>
        /// <param name="id">chave primária da entidade Resultado.</param>
        /// <returns>Retorna uma entidade</returns>
        public Resultado Consultar(int id)
        {
            Resultado entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Resultado
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Resultado Inserir(Resultado entidade)
        {
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Resultado.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Resultado entidade)
        {
            Resultado entidadeConsulta = this.Consultar(entidade.IdResultado);
            //entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            //entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Pessoa.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(Resultado entidade)
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

        #region Métodos Personalizado

        public List<Resultado> ListarAtivos()
        {
            List<Resultado> lista = persistencia.ListarAtivos();
            return lista;
        }

        public List<Resultado> ListarResultadosSemPdf()
        {
            List<Resultado> lista = persistencia.ListarResultadosSemPdf();
            return lista;
        }

        #endregion
    }
}