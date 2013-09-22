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
    /// Classe de negócio da entidade: MenuOpcoesPerfis
    /// </summary>
    public class MenuOpcoesPerfisBll
    {
        #region Campos privados

        MenuOpcoesPerfisDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public MenuOpcoesPerfisBll()
        {
            persistencia = new MenuOpcoesPerfisDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: MenuOpcoesPerfis
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<MenuOpcoesPerfis> Listar()
        {
            List<MenuOpcoesPerfis> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: MenuOpcoesPerfis
        /// </summary>
        /// <param name="id">chave primária da entidade MenuOpcoesPerfis.</param>
        /// <returns>Retorna uma entidade</returns>
        public MenuOpcoesPerfis Consultar(int id)
        {
            MenuOpcoesPerfis entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: MenuOpcoesPerfis
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public MenuOpcoesPerfis Inserir(MenuOpcoesPerfis entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioMenuOpcoesPerfis.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeMenuOpcoesPerfis.Id);

            entidade.CidadeMenuOpcoesPerfis = entidadeCidades;
            entidade.RamoNegocioMenuOpcoesPerfis = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: MenuOpcoesPerfis.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(MenuOpcoesPerfis entidade)
        {
            MenuOpcoesPerfis entidadeConsulta = this.Consultar(entidade.IdMenuOpcoesPerfis);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        public List<MenuOpcoesPerfis> ListarAtivos()
        {
            List<MenuOpcoesPerfis> lista = persistencia.ListarAtivos();
            return lista;
        }


        /*public List<MenuOpcoesPerfis> ListarMenuUsr(string Login)
        {
            List<MenuOpcoesPerfis> lista = persistencia.ListarMenuUsr(Login);
            return lista;
        }*/

        #endregion
    }
}