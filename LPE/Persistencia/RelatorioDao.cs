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
    /// Classe de Persistência da entidade Laudo
    /// </summary>
    public class RelatorioDao : Crud<Relatorio>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public RelatorioDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Laudo
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Relatorio> Listar()
        {
            List<Relatorio> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Laudo
        /// </summary>
        /// <param name="id">chave primária da entidade Laudo.</param>
        /// <returns>Retorna uma entidade</returns>
        public Relatorio Consultar(int id)
        {
            Relatorio entidade = Contexto.Consultar(a => a.IdRelatorio == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Laudo
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Relatorio Incluir(Relatorio entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Laudo.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Relatorio entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Laudo.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Relatorio entidade)
        {
            return Contexto.Excluir(entidade);
        }

        /// <summary>
        /// Método para exclusão lógica de entidade do tipo: Pessoa.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a exclusão.</returns>
        public bool ExcluirLogico(Relatorio entidade)
        {
            return Contexto.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<Relatorio> ListarAtivos()
        {
            List<Relatorio> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public List<Relatorio> ListarRelatorioPorValor(double Valor, int idGrupo)
        {
            //List<Relatorio> lista = Contexto.Listar(a => a.IdGrupo.IdGrupo == idGrupo && (a.ValorMin <= Valor && a.ValorMax >= Valor)).ToList();
            List<Relatorio> lista = Contexto.Listar(a => a.IdGrupo.IdGrupo == idGrupo && a.ValorMin <= Valor).ToList();
            return lista;
        }

        #endregion
    }
}
