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
    /// Classe de negócio da entidade: Resposta
    /// </summary>
    public class RespostaBll
    {
        #region Campos privados

        RespostaDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public RespostaBll()
        {
            persistencia = new RespostaDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Resposta
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Resposta> Listar()
        {
            List<Resposta> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Resposta
        /// </summary>
        /// <param name="id">chave primária da entidade Resposta.</param>
        /// <returns>Retorna uma entidade</returns>
        public Resposta Consultar(int id)
        {
            Resposta entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Resposta
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Resposta Inserir(Resposta entidade)
        {
            int usrI = entidade.idUsuario.IdUsuario;

            try
            {
                entidade.DataResposta = DateTime.Now;
                entidade.HoraReposta = Convert.ToDateTime(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "." + DateTime.Now.Millisecond);
                return persistencia.Incluir(entidade);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro durante o processamento da requisição.\n usrI:" + usrI + "Message:" + e.Message + "Inner Exception: " + e.InnerException + "Source: " + e.Source + "Data: " + e.Data);
            }
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Resposta.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Resposta entidade)
        {
            Resposta entidadeConsulta = this.Consultar(entidade.IdResposta);
            return persistencia.Alterar(entidade);
        }

        #endregion
    }
}