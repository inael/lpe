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
    /// Classe de negócio da entidade: OpcaoRespostaToQuestionario
    /// </summary>
    public class OpcaoRespostaToQuestionarioBll
    {
        #region Campos privados

        OpcaoRespostaToQuestionarioDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public OpcaoRespostaToQuestionarioBll()
        {
            persistencia = new OpcaoRespostaToQuestionarioDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: OpcaoRespostaToQuestionario
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<OpcaoRespostaToQuestionario> Listar()
        {
            List<OpcaoRespostaToQuestionario> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: OpcaoRespostaToQuestionario
        /// </summary>
        /// <param name="id">chave primária da entidade OpcaoRespostaToQuestionario.</param>
        /// <returns>Retorna uma entidade</returns>
        public OpcaoRespostaToQuestionario Consultar(int id)
        {
            OpcaoRespostaToQuestionario entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: OpcaoRespostaToQuestionario
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public OpcaoRespostaToQuestionario Inserir(OpcaoRespostaToQuestionario entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioOpcaoRespostaToQuestionario.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadeOpcaoRespostaToQuestionario.Id);

            entidade.CidadeOpcaoRespostaToQuestionario = entidadeCidades;
            entidade.RamoNegocioOpcaoRespostaToQuestionario = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: OpcaoRespostaToQuestionario.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(OpcaoRespostaToQuestionario entidade)
        {
            OpcaoRespostaToQuestionario entidadeConsulta = this.Consultar(entidade.IdOpcaoRespostaQuestionario);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        #endregion
        
        #region Métodos personalizados

            public List<OpcaoRespostaToQuestionario> ListarAtivos()
            {
                List<OpcaoRespostaToQuestionario> lista = persistencia.ListarAtivos();
                return lista;
            }

            public IList<OpcaoRespostaToQuestionario> ListarOpcaoRespostaToQuestionario(int idQuestionario)
            {
                List<OpcaoRespostaToQuestionario> lista = persistencia.ListarOpcaoRespostaToQuestionario(idQuestionario);
                return lista;
            }

        #endregion
    }
}