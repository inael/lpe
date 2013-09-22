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
    /// Classe de negócio da entidade: Laudo
    /// </summary>
    public class RelatorioBll
    {
        #region Campos privados

        RelatorioDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public RelatorioBll()
        {
            persistencia = new RelatorioDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Laudo
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Relatorio> Listar()
        {
            List<Relatorio> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Laudo
        /// </summary>
        /// <param name="id">chave primária da entidade Laudo.</param>
        /// <returns>Retorna uma entidade</returns>
        public Relatorio Consultar(int id)
        {
            Relatorio entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Laudo
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Relatorio Inserir(Relatorio entidade)
        {
            //PessoaBll negocioPessoa = new PessoaBll();
            //Pessoa entidadePessoa = negocioPessoa.Consultar(entidade);

            //entidade.Pessoa_Usuario = entidadePessoa;
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Laudo.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Relatorio entidade)
        {
            Relatorio entidadeConsulta = this.Consultar(entidade.IdRelatorio);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.UsuarioAteracao = entidadeConsulta.UsuarioInclusao;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Pessoa.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool ExcluirLogico(Relatorio entidade)
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

        public List<Relatorio> ListarAtivos()
        {
            List<Relatorio> lista = persistencia.ListarAtivos();
            return lista;
        }

        public List<Relatorio> ListarRelatorioPorValor(double Valor, int idGrupo)
        {
            List<Relatorio> lista = persistencia.ListarRelatorioPorValor(Valor, idGrupo);
            return lista;
        }

        #endregion
    }
}