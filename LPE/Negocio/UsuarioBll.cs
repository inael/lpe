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
    /// Classe de negócio da entidade: Usuario
    /// </summary>
    public class UsuarioBll
    {
        #region Campos privados

        UsuarioDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public UsuarioBll()
        {
            persistencia = new UsuarioDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Usuario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Usuario> Listar()
        {
            List<Usuario> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Usuario
        /// </summary>
        /// <param name="id">chave primária da entidade Usuario.</param>
        /// <returns>Retorna uma entidade</returns>
        public Usuario Consultar(int id)
        {
            Usuario entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Usuario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Usuario Inserir(Usuario entidade)
        {
            /*EmpresaBll negocioEmpresa = new EmpresaBll();
            Empresa entidadeEmpresa = negocioEmpresa.Consultar(entidade.EmpresaUsuario.Id);

            MenuGrupoBll negocioMenuGrupo = new MenuGrupoBll();
            MenuGrupo entidadeMenuGrupo = negocioMenuGrupo.Consultar(entidade.MenuGrupoUsuario.Id);*/

            PessoaBll negocioPessoa = new PessoaBll();
            Pessoa entidadePessoa = negocioPessoa.Consultar(entidade.Pessoa_Usuario.IdPessoa);

            /*PefilBll negocioPerfil = new PefilBll();
            Pefil entidadePerfil = negocioPessoa.Consultar(entidade.PerfilUsuario.IdPerfil);*/

            //entidade.EmpresaUsuario = entidadeEmpresa;
            //entidade.MenuGrupoUsuario = entidadeMenuGrupo;
            entidade.Pessoa_Usuario = entidadePessoa;
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Usuario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Usuario entidade)
        {
            Usuario entidadeConsulta = this.Consultar(entidade.IdUsuario);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }
        #endregion
        
        #region Métodos Personalizado
        
        public List<Usuario> ListarAtivos()
        {
            List<Usuario> lista = persistencia.ListarAtivos();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Usuario
        /// </summary>
        /// <param name="Login">Login da entidade Usuario.</param>
        /// <returns>Retorna uma entidade</returns>
        public Usuario ConsultarLogin(string Login)
        {
            Usuario entidade = persistencia.ConsultarLogin(Login);
            return entidade;
        }

        #endregion
    }
}