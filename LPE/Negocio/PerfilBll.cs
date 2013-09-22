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
    /// Classe de negócio da entidade: Perfil
    /// </summary>
    public class PerfilBll
    {
        #region Campos privados

        PerfilDao persistencia;

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão do objeto de negócio.
        /// </summary>
        public PerfilBll()
        {
            persistencia = new PerfilDao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades do tipo: Perfil
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<Perfil> Listar()
        {
            List<Perfil> lista = persistencia.Listar();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: Perfil
        /// </summary>
        /// <param name="id">chave primária da entidade Perfil.</param>
        /// <returns>Retorna uma entidade</returns>
        public Perfil Consultar(int id)
        {
            Perfil entidade = persistencia.Consultar(id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: Perfil
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public Perfil Inserir(Perfil entidade)
        {
            /*RamoNegocioBll negocioRamoNegocio = new RamoNegocioBll();
            RamoNegocio entidadeRamoNegocio = negocioRamoNegocio.Consultar(entidade.RamoNegocioPerfil.Id);

            CidadesBll negocioCidades = new CidadesBll();
            Cidades entidadeCidades = negocioCidades.Consultar(entidade.CidadePerfil.Id);

            entidade.CidadePerfil = entidadeCidades;
            entidade.RamoNegocioPerfil = entidadeRamoNegocio;*/
            entidade.DataInclusao = DateTime.Now;
            entidade.DataAteracao = DateTime.MaxValue;
            return persistencia.Incluir(entidade);
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: Perfil.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(Perfil entidade)
        {
            Perfil entidadeConsulta = this.Consultar(entidade.IdPerfil);
            entidade.UsuarioInclusao = entidadeConsulta.UsuarioInclusao;
            entidade.DataInclusao = entidadeConsulta.DataInclusao;
            return persistencia.Alterar(entidade);
        }

        public List<Perfil> ListarAtivos()
        {
            List<Perfil> lista = persistencia.ListarAtivos();
            return lista;
        }

        #endregion
    }
}