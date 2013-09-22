using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Data;
using Ninject.Modules;
using Ninject;
using Core.Infrastructure;
using Core.Models;

namespace Core.Infrastructure
{
    // Summary:
    //  Item para componente AutoComplete, com key e value
    [Serializable]
    public class AutoCompleteItem
    {
        [Column]
        public string key { get; set; }
        [Column]
        public string value { get; set; }
    }

    // Summary:
    //  Lista para retorno de resultados para a view, com propriedade d
    //  utilizada pelo JCKBAutoComplete
    [Serializable]
    public class AutoCompleteResult
    {
        public List<AutoCompleteItem> d { get; set; }
    }

    // Summary:
    //  Extension method para retornar valores
    public static class AutoComplete
    {
        static private CommonKernel commonk = new CommonKernel();

        // Summary:
        //  Gera instrução SQL para obter os valores do AutoComplete
        private static string GenerateSQL(string table, string key, string value, string whereFields, string joinElement)
        {
            //TODO: Retornar parâmetro em {3}, ao invés de concatenar a string
            //TODO: Verificar se tamanho 100 é suficiente para todos os casos @SearchTerm+'%'
            List<Object> objs = new List<object> { key, value, table, whereFields, joinElement };
            return String.Format("select cast({0} as varchar(100)) as \"key\", cast({1} as varchar(100)) as \"value\" from {2} {4} where {3} like @TAG+'%' ",
                objs.ToArray());
        }

        // Summary:
        //  Abre instrução SQL e preenche lista com resultados obtidos
        private static AutoCompleteResult FillAutoCompleteValues(string table,
            string key, string value, string whereFields, IDbDataParameter filter, string joinElements)
        {
            string SQL = GenerateSQL(table, key, value, whereFields, joinElements);
            IEntityRepository database = (IEntityRepository)commonk.kernel.Get(typeof(IEntityRepository));
            AutoCompleteResult result = new AutoCompleteResult();
            database.Start();
            try
            {
                result.d = database.GetEntities<AutoCompleteItem>(SQL, new IDbDataParameter[] {filter} );
                database.Commit();
            }
            catch (Exception e)
            {
                database.Rollback();
                throw new Exception("Ocorreu um erro durante o processamento da requisição.\n Message:" + e.Message + "Inner Exception:" + e.InnerException);
            }
            return result;
        }

        // Summary:
        //  Retorna os valores de autocomplete da Entidade de domínio, com AutoCompleteKey e AutoCompleteValue
        public static AutoCompleteResult GetAutoCompleteValues<T>(IDbDataParameter filter) where T : new()
        {
            Type inf = typeof(T);

            //Lista de atributos a serem considerados como chaves no autocomplete
            var keyAttribute = from p in inf.GetProperties()
                               let attr = p.GetCustomAttributes(typeof(AutoCompleteKey), false)
                               where attr.Length == 1
                               select ((AutoCompleteKey)attr.First()).Name ?? p.Name;

            //Lista de valores a serem exibidos ao usuário no processo de autocomplete
            var valuesAttribute = from p in inf.GetProperties()
                                  let attr = p.GetCustomAttributes(typeof(AutoCompleteValue), false)
                                  where attr.Length == 1
                                  select ((AutoCompleteValue)attr.First()).Name ?? p.Name;

            if (keyAttribute.Count() == 0 || valuesAttribute.Count() == 0)
                throw new IndexOutOfRangeException("A classe de domínio não contém os attributos necessários");


            return FillAutoCompleteValues(
                DatabaseUtils.GetTableOfObject(inf),
                buildKeyString(inf, keyAttribute.ToList()),
                buildValueString(inf, valuesAttribute.ToList() ),
                buildWhereString(inf, valuesAttribute.First()), 
                filter,
                buildJoinString(inf)
                ); 
        }

        private static String buildWhereString(Type inf, String colFiltered )
        {
            String whereFields = " ";

            List<String> whereFieldAttributes = (from p in inf.GetProperties()
                                         let attr = p.GetCustomAttributes(typeof(WhereElement), false)
                                         where attr.Length == 1
                                         select ((WhereElement)attr.First()).Expression).ToList<String>();

            if (whereFieldAttributes.Count != 0)
            {
                whereFields += whereFieldAttributes[0];
                for (int i = 1; i < whereFieldAttributes.Count; ++i)
                {
                    whereFields += " and" + whereFieldAttributes[i];
                }
                whereFields += " and ";
            }
            whereFields += colFiltered;
            
            return whereFields;
        }

        private static String buildJoinString(Type inf)
        {
            String tableName = DatabaseUtils.GetTableOfObject(inf);
            String join = "";

            var innerJoinTableAttribute = inf.GetCustomAttributes(typeof(InnerJoinTable), false);

            List<String> foreignTableName = innerJoinTableAttribute.ToList().Count != 0 ? ((InnerJoinTable)innerJoinTableAttribute.First()).Tables.ToList<String>() : new List<String>();


            //Constroi os inner join's
            for (int j = 0; j < foreignTableName.Count; ++j)
            {
                var foreignKeyAttribute = from p in inf.GetProperties()
                                          let attr = p.GetCustomAttributes(typeof(ForeignKey), false)
                                          where attr.Length == 1 && ((ForeignKey)attr.First()).Name == foreignTableName[j]
                                          select p.Name;

                var destinations = from p in inf.GetProperties()
                                          let attr = p.GetCustomAttributes(typeof(ForeignKey), false)
                                          where attr.Length == 1 && ((ForeignKey)attr.First()).Name == foreignTableName[j]
                                          select ((ForeignKey)attr.First()).Destination;


                join += buildInnerJoinString(foreignTableName[j], foreignKeyAttribute.ToList<String>(), destinations.ToList<String>(), tableName);

            }

            //TODO Desenvolver códigos para left joins

            return join;
        }

        private static String buildKeyString(Type inf, List<String> listKey)
        {
            String tableName = DatabaseUtils.GetTableOfObject(inf);

            string keys = String.Format("cast( {1}.{0} as varchar)", listKey[0], tableName);
            for (int j = 1; j < listKey.Count; ++j)
                keys += String.Format(" + ' - ' + cast( {1}.{0} as varchar)", listKey[j], tableName);

            return keys;
        }

        private static String buildValueString(Type inf, List<String> listValues)
        {
            String tableName = DatabaseUtils.GetTableOfObject(inf);

            var innerJoinTableAttribute = inf.GetCustomAttributes(typeof(InnerJoinTable), false);

            List<String> foreignTableName = innerJoinTableAttribute.ToList().Count != 0 ? ((InnerJoinTable)innerJoinTableAttribute.First()).Tables.ToList<String>() : new List<String>();

            List<String> listForeignValues = (from p in inf.GetProperties()
                                         let attr = p.GetCustomAttributes(typeof(ForeignElement), false)
                                         where attr.Length == 1
                                         select ((ForeignElement)attr.First()).Name ?? p.Name).ToList<String>();

            string vals = String.Format("cast( {1}.{0} as varchar)", listValues[0], tableName);
            for (int j = 1; j < listValues.Count; ++j)
                vals += String.Format(" + ' - ' + cast( {1}.{0} as varchar)", listValues[j], tableName);

            if (foreignTableName.Count != 0)
            {
                vals += String.Format(" + ' - ' + cast( {0} as varchar)", listForeignValues[0]);
                for (int j = 1; j < listForeignValues.Count; ++j)
                    vals += String.Format(" + ' - ' + cast( {0} as varchar)", listForeignValues[j]);

                return vals;
            }
            return vals;
        }

        //Constroi os strings de inner join
        private static String buildInnerJoinString(String joinTableName, List<String> foreignKeyAttribute, List<String> destination, String tableName)
        {
            string joinable = "";

            joinable = String.Format("inner join {0} on ( ", joinTableName);
            List<String> fKeys = foreignKeyAttribute.ToList();

            if(destination[0] == null)  joinable += String.Format("{0}.{1} = {2}.{1} ", joinTableName, fKeys[0], tableName);
            else joinable += String.Format("{0}.{1} = {2}.{1} ", joinTableName, fKeys[0], destination[0]);

            for (int i = 1; i < fKeys.Count; ++i)
            {
                if (destination[i] == null)  joinable += String.Format("and {0}.{1} = {2}.{1} ", joinTableName, fKeys[i], tableName);
                else joinable += String.Format("and {0}.{1} = {2}.{1} ", joinTableName, fKeys[i], destination[i]);
            }
            
            joinable += " ) ";

            return joinable;

        }
    }
}
