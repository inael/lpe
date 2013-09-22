/*
* Framework de Acesso a Dados utilizando NHibernate
* Arquiteto: José Lino Neto - joselinoneto@gmail.com
*/

#region Referências

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using Fabrica;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject;

#endregion

namespace FabricaNhibernate
{
    /// <summary>
    /// Classe de acesso a dados utilizando NHibernate.
    /// </summary>
    /// <typeparam name="T">Tipo definido durante execução.</typeparam>
    public class ContextoNh<T> : ICrud<T> where T : class
    {
        #region Propriedade Estatica

        public static ISessionFactory ConstruirFabrica
        {
            get
            {
                ISessionFactory fabrica;
                string stringConexao = ConfigurationManager.AppSettings["stringConexao"];
                fabrica = Fluently.Configure()
                       .ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"))
                       .Database(MsSqlConfiguration.MsSql2008
                       .ShowSql()
                       .FormatSql()
                       .ConnectionString(stringConexao))
                       .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(Modelo.Entity).Assembly)
                                                      .AddFromAssembly(typeof(AutoCompleteModel.Entity).Assembly))
                       .BuildSessionFactory();

                return fabrica;
            }
        }

        #endregion

        #region Campos Privados

        /// <summary>
        /// Campo privado para utilização da sessão.
        /// </summary>
        private ISession _session;

        #endregion

        #region Propriedades

        /// <summary>
        /// Propriedade com a sessão utilizada pelo NHibernate.
        /// </summary>
        public ISession Sessao
        {
            get
            {
                return _session;
            }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor passando a sessão como parametro.
        /// </summary>
        /// <param name="sessao">Sessão utilizada na aplicação.</param>
        [Inject]
        public ContextoNh(ISession sessao)
        {
            _session = sessao;
            if (!sessao.IsOpen)
            {
                _session = sessao.SessionFactory.OpenSession();
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para listar todas as entidades. Cuidado com a quantidade de registros.
        /// </summary>
        /// <returns>Retorna uma lista de entidades.</returns>
        public virtual IEnumerable<T> Listar()
        {
            var lista = _session.QueryOver<T>()
                        .List();

            return lista;
        }

        /// <summary>
        /// Método para listar todos registros páginados.
        /// </summary>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <param name="primeiroResultado">Primeiro resultado da página.</param>
        /// <returns>Retorna uma lista.</returns>
        public virtual IEnumerable<T> Listar(int tamanhoPagina, int primeiroResultado)
        {

            var lista = _session.QueryOver<T>()
                        .Take(tamanhoPagina).Skip(primeiroResultado)
                        .List();

            return lista;

        }

        /// <summary>
        /// Método para listar todos registros a partir de um filtro páginado.
        /// </summary>
        /// <param name="filtro">Filtro de pesquisa.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <param name="primeiroResultado">Primeiro resultado da página.</param>
        /// <returns></returns>
        public IEnumerable<T> Listar(Expression<Func<T, bool>> filtro, int tamanhoPagina, int primeiroResultado)
        {
            try
            {
                var lista = _session.QueryOver<T>()
                            .Where(filtro)
                            .Take(tamanhoPagina).Skip(primeiroResultado)
                            .List();

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para listar os registros ordenados e páginados.
        /// </summary>
        /// <param name="ordem">Propriedade para servir de critério de ordenação.</param>
        /// <param name="fluxo">Fluxo Crescente ou Descrecente. Utilizar Asc ou Desc.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <param name="primeiroResultado">Primeiro resultado da página.</param>
        /// <returns></returns>
        public IEnumerable<T> Listar(Expression<Func<T, object>> ordem, string fluxo, int tamanhoPagina, int primeiroResultado)
        {
            try
            {
                switch (fluxo)
                {
                    case "Asc":
                        {
                            var lista = _session.QueryOver<T>()
                                        .OrderBy(ordem).Asc
                                        .Take(tamanhoPagina).Skip(primeiroResultado)
                                        .List();

                            return lista;
                        }
                    default:
                        {
                            var lista = _session.QueryOver<T>()
                                .OrderBy(ordem).Desc
                                .Take(tamanhoPagina).Skip(primeiroResultado)
                                .List();

                            return lista;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Listar registros a partir do filtro, ordenado e páginado.
        /// </summary>
        /// <param name="filtro">Filtro de pesquisa.</param>
        /// <param name="ordem">Ordenação.</param>
        /// <param name="fluxoOrdem">Fluxo Crescente ou Descrecente. Utilizar Asc ou Desc.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <param name="primeiroResultado">Primeiro resultado da página.</param>
        /// <returns></returns>
        public IEnumerable<T> Listar(Expression<Func<T, bool>> filtro, Expression<Func<T, object>> ordem, string fluxoOrdem, int tamanhoPagina, int primeiroResultado)
        {
            try
            {
                switch (fluxoOrdem)
                {
                    case "Asc":
                        {
                            var lista = _session.QueryOver<T>()
                                        .Where(filtro)
                                        .OrderBy(ordem).Asc
                                        .Take(tamanhoPagina).Skip(primeiroResultado)
                                        .List();

                            return lista;
                        }
                    default:
                        {
                            var lista = _session.QueryOver<T>()
                                .Where(filtro)
                                .OrderBy(ordem).Desc
                                .Take(tamanhoPagina).Skip(primeiroResultado)
                                .List();

                            return lista;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para listar as entidades de acordo com os filtros.
        /// </summary>
        /// <param name="filtro">Filtros de consulta.</param>
        /// <returns>Retorna uma lista de entidades.</returns>
        public virtual IEnumerable<T> Listar(Expression<Func<T, bool>> filtro)
        {
            var lista = _session.QueryOver<T>()
                        .Where(filtro)
                        .List();

            return lista;
        }

        /// <summary>
        /// Método para listar todas as entidades e com alguma ordem e seu fluxo. Cuidado com a quantidade de registros.
        /// </summary>
        /// <param name="ordem">Campo para ser utilizado como ordem.</param>
        /// <param name="fluxoOrdem">Fluxo de ordenação. Utilize Asc para crescente e Des para decrescente.</param>
        /// <returns>Retorna uma lista de entidades.</returns>
        public IEnumerable<T> Listar(Expression<Func<T, object>> ordem, string fluxoOrdem)
        {
            switch (fluxoOrdem)
            {
                case "Asc":
                    {
                        var lista = _session.QueryOver<T>()
                        .OrderBy(ordem).Asc
                        .List();

                        return lista;
                    }
                default:
                    {
                        var lista = _session.QueryOver<T>()
                        .OrderBy(ordem).Desc
                        .List();

                        return lista;
                    }
            }
        }

        /// <summary>
        /// Método para listar as entidades de acordo com os filtros e  e com alguma ordem e seu fluxo.
        /// </summary>
        /// <param name="filtro">Filtros de consulta.</param>
        /// <param name="ordem">Campo para ser utilizado como ordem.</param>
        /// <param name="fluxoOrdem">Fluxo de ordenação. Utilize Asc para crescente e Des para decrescente.</param>
        /// <returns>Retorna uma lista de entidades.</returns>
        public IEnumerable<T> Listar(Expression<Func<T, bool>> filtro, Expression<Func<T, object>> ordem, string fluxoOrdem)
        {
            switch (fluxoOrdem)
            {
                case "Asc":
                    {
                        var lista = _session.QueryOver<T>()
                        .Where(filtro)
                        .OrderBy(ordem).Asc
                        .List();

                        return lista;
                    }
                default:
                    {
                        var lista = _session.QueryOver<T>()
                        .Where(filtro)
                        .OrderBy(ordem).Desc
                        .List();

                        return lista;
                    }
            }
        }

        /// <summary>
        /// Método para incluir uma nova entidade.
        /// </summary>
        /// <param name="entidade">Entidade a ser incluida.</param>
        /// <returns>Retorna a própria entidade com sua chave primária preenchida.</returns>
        public virtual T Incluir(T entidade)
        {
            using (_session.BeginTransaction())
            {
                try
                {
                    _session.Save(entidade);
                    _session.Transaction.Commit();
                    return entidade;
                }
                catch (Exception)
                {
                    _session.Transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Método para consultar uma entidade.
        /// </summary>
        /// <param name="filtro">Filtro a ser utilizado para consulta.</param>
        /// <returns>Retorna uma entidade.</returns>
        public T Consultar(Expression<Func<T, bool>> filtro)
        {
            T entidade = _session.QueryOver<T>()
                        .Where(filtro)
                        .SingleOrDefault();

            return entidade;
        }

        /// <summary>
        /// Método para excluir uma entidade.
        /// </summary>
        /// <param name="entidade">Entidade a ser excluida.</param>
        /// <returns>Retorna verdadeiro quando a transação da exclusão for concluida com sucesso.</returns>
        public virtual bool Excluir(T entidade)
        {
            using (_session.BeginTransaction())
            {
                try
                {
                    _session.Delete(entidade);
                    _session.Transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    _session.Transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Método para alterar uma entidade.
        /// </summary>
        /// <param name="entidade">Entidade a ser alterada.</param>
        /// <returns>Retorna verdadeiro quando a transação de alteração for concluida.</returns>
        public virtual bool Alterar(T entidade)
        {
            using (_session.BeginTransaction())
            {
                try
                {
                    T objetoMerge = (T)_session.Merge(entidade);
                    _session.Update(objetoMerge);
                    _session.Transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    _session.Transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Método para executar comando escrito em SQL.
        /// </summary>
        /// <param name="sql">Comando em SQL</param>
        /// <returns>Retorna uma lista.</returns>
        public List<T> ExecutarSqlListagem<T>(string sql) where T : new()
        {
            IList<T> lista;
            
            ISQLQuery query = this.Sessao.CreateSQLQuery(sql)
                .AddEntity(typeof(T).ToString(), typeof(T));

            lista = query.List<T>();

            List<T> list = (List<T>)lista;

            return list;
        }

        /// <summary>
        /// Método para executar uma storage procedure.
        /// </summary>
        /// <param name="nomeProcedure">Nome da storage procedure no banco.</param>
        /// <param name="listaParametros">Lista de Parametros com nome do parametro na stored procedure e seu valor.</param>
        /// <returns>Retorna uma lista.</returns>
        protected IList<T> ExecutarStoredProcedure(string nomeProcedure, Parametro[] listaParametros)
        {
            IQuery query = this.Sessao.GetNamedQuery("ListarTodosPontosPorUsuario");

            foreach (var item in listaParametros)
            {
                switch (item.Valor.GetType().ToString())
                {
                    case "System.String":
                        {
                            query.SetString(item.NomeParametro, item.Valor.ToString());
                        } break;
                    case "System.Int32":
                        {
                            query.SetInt32(item.NomeParametro, (int)item.Valor);
                        } break;
                    case "System.DateTime":
                        {
                            query.SetDateTime(item.NomeParametro, (DateTime)item.Valor);
                        } break;
                }

            }

            IList<T> lista = query.List<T>();

            return lista;
        }

        #endregion
    }
}