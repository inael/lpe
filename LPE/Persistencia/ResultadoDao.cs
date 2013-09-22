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
    /// Classe de Persistência da entidade Resultado
    /// </summary>
    public class ResultadoDao : Crud<Resultado>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public ResultadoDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: Resultado
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Resultado> Listar()
        {
            List<Resultado> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Resultado
        /// </summary>
        /// <param name="id">chave primária da entidade Resultado.</param>
        /// <returns>Retorna uma entidade</returns>
        public Resultado Consultar(int id)
        {
            Resultado entidade = Contexto.Consultar(a => a.IdResultado == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Resultado
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Resultado Incluir(Resultado entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Resultado.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Resultado entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: Resultado.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(Resultado entidade)
        {
            return Contexto.Excluir(entidade);
        }

        public bool ExcluirLogico(Resultado entidade)
        {
            return Contexto.Alterar(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<Resultado> ListarAtivos()
        {
            List<Resultado> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        public List<Resultado> ListarResultadosSemPdf()
        {
            List<Resultado> lista = Contexto.Listar(a => a.Pdf == false).ToList();
            return lista;
        }

        #endregion
    }
}
