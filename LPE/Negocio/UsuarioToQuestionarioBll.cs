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
    /// Classe de negócio da entidade: UsuarioQuestionario
    /// </summary>
    public class UsuarioToQuestionarioBll
    {
        #region Campos privados

        UsuarioToQuestionarioDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public UsuarioToQuestionarioBll()
        {
            persistencia = new UsuarioToQuestionarioDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: UsuarioQuestionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<UsuarioToQuestionario> Listar()
        {
            List<UsuarioToQuestionario> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: UsuarioQuestionario
        /// </summary>
        /// <param name="id">chave primária da entidade UsuarioQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public UsuarioToQuestionario Consultar(int id)
        {
            UsuarioToQuestionario entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: UsuarioQuestionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public UsuarioToQuestionario Inserir(UsuarioToQuestionario entidade)
        {
            /*EmpresaBll negocioEmpresa = new EmpresaBll();
            Empresa entidadeEmpresa = negocioEmpresa.Consultar(entidade.EmpresaUsuarioQuestionario.Id);

            MenuGrupoBll negocioMenuGrupo = new MenuGrupoBll();
            MenuGrupo entidadeMenuGrupo = negocioMenuGrupo.Consultar(entidade.MenuGrupoUsuarioQuestionario.Id);*/

            PessoaBll negocioPessoa = new PessoaBll();
            //Pessoa entidadePessoa = negocioPessoa.Consultar(entidade.Pessoa_UsuarioQuestionario.IdPessoa);

            /*PefilBll negocioPerfil = new PefilBll();
            Pefil entidadePerfil = negocioPessoa.Consultar(entidade.PerfilUsuarioQuestionario.IdPerfil);*/

            //entidade.EmpresaUsuarioQuestionario = entidadeEmpresa;
            //entidade.MenuGrupoUsuarioQuestionario = entidadeMenuGrupo;
            //entidade.Pessoa_UsuarioQuestionario = entidadePessoa;
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: UsuarioQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(UsuarioToQuestionario entidade)
        {
            UsuarioToQuestionario entidadeConsulta = this.Consultar(entidade.IdUsuarioQuestionario);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            entidade.UsuarioAteracao = entidadeConsulta.idUsuario.Pessoa_Usuario.NomePessoa;
            entidade.DataAteracao = DateTime.Now;
            return persistencia.Alterar(entidade);
        }
        #endregion

        #region Métodos Personalizado

        public List<UsuarioToQuestionario> ListarAtivos()
        {
            List<UsuarioToQuestionario> lista = persistencia.ListarAtivos();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: UsuarioQuestionario
        /// </summary>
        /// <param name="Login">Login da entidade UsuarioQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public List<UsuarioToQuestionario> GetUserQuestionario(string Login)
        {
            List<UsuarioToQuestionario> lista = persistencia.GetUserQuestionario(Login);
            return lista;
        }

        public List<UsuarioToQuestionario> isNotQuestComplete()
        {
            List<UsuarioToQuestionario> lista = persistencia.isNotQuestComplete();
            return lista;
        }

        #endregion
    }
}