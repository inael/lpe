using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Infrastructure;
using Cockpit.Utils;
using System.Data.SqlClient;
using System.Data;
using Cockpit.Models;
using Common;
using Ninject;

namespace Cockpit.Handler
{
    public static class UserPermissionHandler 
    {
        //
        // GET: /PermissionFilterAttribute/
        private static CommonKernel commonk = new CommonKernel();

        public static String UserQueryNonCoalesce = @" AND ( R.MUNI_REV in (
			 select MRC.CODI_MNR
				from USUARIOS U
				inner join USUARIOSPAPEIS UP on (UP.CODI_USU = U.CODI_USU)
				inner join REGIAOPAPEL MRP on (MRP.CODI_PAP = UP.CODI_PAP)
				inner join REGIAOCOMPOSICAO MRC on (MRC.CODI_REG = MRP.CODI_MNR )
				where U.CODI_USU = @CODI_USU           
			)  
            ) 
            AND R.CODI_REV in (
            select RP.CODI_REV
				from USUARIOS U
				inner join USUARIOSPAPEIS UP on (UP.CODI_USU = U.CODI_USU)
				inner join REVENDAPAPEL RP on (RP.CODI_PAP = UP.CODI_PAP) 
				where U.CODI_USU = @CODI_USU
            )";

        public static void SetUserPermission()
        {
            GetAllowedDistributors();
        }

        public static PermissionDenied GetPermissionObject
            (List<int> distributorList, List<String> citiesList , Object result)
        {
            return new PermissionDenied
            {
                CitiesDenied = GetNotAllowedCities(citiesList),
                DistributorsDenied = GetNotAllowedDistributors(distributorList),
                Result = result
            };
        }


        private static List<City> GetNotAllowedCities(List<String> RequestedCities)
        {
            IEntityRepository database = (IEntityRepository)commonk.kernel.Get(typeof(IEntityRepository));
            List<City> requested = new List<City>();

            String queryAllowedCities = @"select distinct MRC.CODI_MNR IBGE_MUN
				from USUARIOS U
				inner join USUARIOSPAPEIS UP on (UP.CODI_USU = U.CODI_USU)
				inner join REGIAOPAPEL MRP on (MRP.CODI_PAP = UP.CODI_PAP)
				inner join REGIAOCOMPOSICAO MRC on (MRC.CODI_REG = MRP.CODI_MNR )
				where U.CODI_USU = @CODI_USU";

            List<City> notAllowewdCities;
            database.Start();
            try
            {
                User user = (User)System.Web.HttpContext.Current.Session["user"];
                List<SqlParameter> ParametersAllowedCities = new List<SqlParameter>();
                ParametersAllowedCities.Add(new SqlParameter { ParameterName = "@CODI_USU", SqlDbType = SqlDbType.Int, Value = user.Code });
                List<City> AllowedCities = database.GetEntities<City>(queryAllowedCities, ParametersAllowedCities.ToArray());


                foreach (var city in RequestedCities)
                    requested.Add(new City { codi_mnr = city });

                notAllowewdCities =
                    (from c in requested
                     where !AllowedCities.Any(item2 => item2.codi_mnr == c.codi_mnr)
                     select c).ToList();

                    if (notAllowewdCities.Count != 0)
                    {
                        String query = @"Select M.NOME_MUN, M.IBGE_MUN FROM MUNICIPIO M WHERE {0}";
                        String queryaux = "";

                        queryaux += String.Format(" ( M.IBGE_MUN = '{0}'", notAllowewdCities[0].codi_mnr);
                        for (int j = 1; j < notAllowewdCities.Count; ++j)
                            queryaux += String.Format(" OR M.IBGE_MUN = '{0}'", notAllowewdCities[j].codi_mnr);
                        queryaux += " )";
                    
                        List<SqlParameter> Parameters = new List<SqlParameter>();
                        String content = String.Format(query, queryaux);
                        notAllowewdCities = database.GetEntities<City>(content, Parameters.ToArray());
                
                    }
                database.Commit();
            }
            catch (Exception e)
            {
                database.Rollback();
                throw new CockpitException("Ocorreu um erro durante o processamento da requisição.\n Message:" + e.Message + "Inner Exception:" + e.InnerException);
            }
            return notAllowewdCities;
        }

        private static List<DistributorModelView> GetNotAllowedDistributors(List<int> RequestedDistributors)
        {
            IEntityRepository database = (IEntityRepository)commonk.kernel.Get(typeof(IEntityRepository));
            List<DistributorModelView> requested = new List<DistributorModelView>();

            List<DistributorModelView> AllowedDistributors = (List<DistributorModelView>)System.Web.HttpContext.Current.Session["distributors"];

            foreach (var distributor in RequestedDistributors)
                requested.Add(new DistributorModelView { DistributorCode = Convert.ToInt32(distributor) });

            List<DistributorModelView> notAllowewdDistributor =
                (from c in requested
                 where !AllowedDistributors.Any(item2 => item2.DistributorCode == c.DistributorCode)
                 select c).ToList();


            if (notAllowewdDistributor.Count != 0)
            {
                String query = @"Select R.CODI_REV, R.RAZA_REV FROM REVENDA R WHERE {0}";
                String queryaux = "";

                queryaux += String.Format("  ( R.CODI_REV = {0}", notAllowewdDistributor[0].DistributorCode);
                for (int j = 1; j < notAllowewdDistributor.Count; ++j)
                    queryaux += String.Format(" OR R.CODI_REV = {0}", notAllowewdDistributor[j].DistributorCode);
                queryaux += " )";

                database.Start();
                try
                {
                    List<SqlParameter> Parameters = new List<SqlParameter>();
                    String content = String.Format(query, queryaux);
                    notAllowewdDistributor = database.GetEntities<DistributorModelView>(content, Parameters.ToArray());
                    database.Commit();
                }
                catch (Exception e)
                {
                    database.Rollback();
                    throw new CockpitException("Ocorreu um erro durante o processamento da requisição.\n Message:" + e.Message + "Inner Exception:" + e.InnerException);
                }
            }

            return notAllowewdDistributor;
        }

        

        private static void GetAllowedDistributors()
        {
            IEntityRepository database = (IEntityRepository)commonk.kernel.Get(typeof(IEntityRepository));

            String query = @"select DISTINCT REV.CODI_REV, REV.RAZA_REV
				from USUARIOS U
				inner join USUARIOSPAPEIS UP on (UP.CODI_USU = U.CODI_USU)
				inner join PAPEL P on (P.CODI_PAP = UP.CODI_PAP)
				inner join REVENDAPAPEL RP on (RP.CODI_PAP = P.CODI_PAP)
				inner join REVENDA REV on (REV.CODI_REV = RP.CODI_REV)       
				where U.CODI_USU = @CODI_USU";

            List<DistributorModelView> allowed;
            database.Start();
            try
            {
                User user = (User)System.Web.HttpContext.Current.Session["user"];
                List<SqlParameter> Parameters = new List<SqlParameter>();
                Parameters.Add(new SqlParameter { ParameterName = "@CODI_USU", SqlDbType = SqlDbType.Int, Value = user.Code });
                allowed = database.GetEntities<DistributorModelView>(query, Parameters.ToArray());
                database.Commit();
            }
            catch (Exception e)
            {
                database.Rollback();
                throw new CockpitException("Ocorreu um erro durante o processamento da requisição.\n Message:" + e.Message + "Inner Exception:" + e.InnerException);
            }
             
            System.Web.HttpContext.Current.Session["distributors"] = allowed;
        }
    }
}
