/*
 * Classe de persistência
 * Arquiteto: José Lino Neto
 * Desenvolvedor: 
 * 
 */

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using Modelo;

#endregion

namespace Persistencia
{
    /// <summary>
    /// Classe de Persistência da entidade MenuOpcao
    /// </summary>
    public class MenuOpcaoDao : Crud<MenuOpcao>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public MenuOpcaoDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: MenuOpcao
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<MenuOpcao> Listar()
        {
            List<MenuOpcao> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: MenuOpcao
        /// </summary>
        /// <param name="id">chave primária da entidade MenuOpcao.</param>
        /// <returns>Retorna uma entidade</returns>
        public MenuOpcao Consultar(int id)
        {
            MenuOpcao entidade = Contexto.Consultar(a => a.IdMenuOpc == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: MenuOpcao
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public MenuOpcao Incluir(MenuOpcao entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: MenuOpcao.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(MenuOpcao entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: MenuOpcao.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(MenuOpcao entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<MenuOpcao> ListarAtivos()
        {
            List<MenuOpcao> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public IList<MenuOpcao> ListarMenuUsr(string Sql)
        {
            IList<MenuOpcao> lista = Contexto.ExecutarSqlListagem<MenuOpcao>(Sql);

            return lista;
        }

        #endregion
    }
}
