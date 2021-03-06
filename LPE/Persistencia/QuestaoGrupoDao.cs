﻿/*
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
    /// Classe de Persistência da entidade QuestaoGrupo
    /// </summary>
    public class QuestaoGrupoDao : Crud<QuestaoGrupo>
    {
        #region Construtores

        /// <summary>
        /// Construtor padrão passando para a classe base qual banco de operação.
        /// </summary>
        public QuestaoGrupoDao()
        {
        }

        #endregion

        #region Propridades

        #endregion

        #region Métodos padrões

        /// <summary>
        /// Método para listar todas as entidades do tipo: QuestaoGrupo
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public List<QuestaoGrupo> Listar()
        {
            List<QuestaoGrupo> lista = Contexto.Listar().ToList();
            return lista;
        }

        /// <summary>
        /// Método para consultar uma entidade do tipo: QuestaoGrupo
        /// </summary>
        /// <param name="id">chave primária da entidade QuestaoGrupo.</param>
        /// <returns>Retorna uma entidade</returns>
        public QuestaoGrupo Consultar(int id)
        {
            QuestaoGrupo entidade = Contexto.Consultar(a => a.IdGrupo == id);
            return entidade;
        }

        /// <summary>
        /// Método para incluir uma entidade do tipo: QuestaoGrupo
        /// </summary>
        /// <param name="entidade">Entidade a ser inserida.</param>
        /// <returns>Retorna a entidade com a chave primaria definida.</returns>
        public QuestaoGrupo Incluir(QuestaoGrupo entidade)
        {
            Contexto.Incluir(entidade);
            return entidade;
        }

        /// <summary>
        /// Método para alterar uma entidade do tipo: QuestaoGrupo.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a alteração.</returns>
        public bool Alterar(QuestaoGrupo entidade)
        {
            return Contexto.Alterar(entidade);
        }

        /// <summary>
        /// Método para excluir uma entidade do tipo: QuestaoGrupo.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro ou falso se houve a excluida.</returns>
        public bool Excluir(QuestaoGrupo entidade)
        {
            return Contexto.Excluir(entidade);
        }

        #endregion

        #region Métodos personalizados

        public List<QuestaoGrupo> ListarAtivos()
        {
            List<QuestaoGrupo> lista = Contexto.Listar(a => a.Excluido == false).ToList();
            return lista;
        }

        #endregion
    }
}
