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
    /// Classe de negócio da entidade: MenuOpcao
    /// </summary>
    public class MenuOpcaoBll
    {
        #region Campos privados

        MenuOpcaoDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public MenuOpcaoBll()
        {
            persistencia = new MenuOpcaoDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: MenuOpcao
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<MenuOpcao> Listar()
        {
            List<MenuOpcao> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: MenuOpcao
        /// </summary>
        /// <param name="id">chave primária da entidade MenuOpcao.</param>
        /// <returns>Retorna uma entidade</returns>
        public MenuOpcao Consultar(int id)
        {
            MenuOpcao entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: MenuOpcao
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public MenuOpcao Inserir(MenuOpcao entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioMenuOpcao.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeMenuOpcao.Id);

            entidade.CidadeMenuOpcao = entidadeCidades;
            entidade.RamoNegocioMenuOpcao = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: MenuOpcao.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(MenuOpcao entidade)
        {
            MenuOpcao entidadeConsulta = this.Consultar(entidade.IdMenuOpc);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        #endregion
        
    #region Métodos personalizados

        public List<MenuOpcao> ListarAtivos()
        {
            List<MenuOpcao> lista = persistencia.ListarAtivos();
            return lista;
        }

        public IList<MenuOpcao> ListarMenuUsr(string Sql)
        {
            IList<MenuOpcao> lista = persistencia.ListarMenuUsr(Sql);
            return lista;
        }

    #endregion
    }
}