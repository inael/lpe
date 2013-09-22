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
    /// Classe de negócio da entidade: Questionario
    /// </summary>
    public class QuestionarioBll
    {
        #region Campos privados

        QuestionarioDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public QuestionarioBll()
        {
            persistencia = new QuestionarioDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Questionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Questionario> Listar()
        {
            List<Questionario> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Questionario
        /// </summary>
        /// <param name="id">chave primária da entidade Questionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public Questionario Consultar(int idQuestionario)
        {
            Questionario entidade = persistencia.Consultar(idQuestionario);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Questionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Questionario Inserir(Questionario entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioQuestionario.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeQuestionario.Id);

            entidade.CidadeQuestionario = entidadeCidades;
            entidade.RamoNegocioQuestionario = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Questionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Questionario entidade)
        {
            Questionario entidadeConsulta = this.Consultar(entidade.IdQuestionario);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        #endregion
        
        #region Métodos personalizados

            public List<Questionario> ListarAtivos()
            {
                List<Questionario> lista = persistencia.ListarAtivos();
                return lista;
            }

        #endregion
    }
}