using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Modelo;

namespace AutoCompleteModel
{
    public class RelatorioGrupoCompleteMap : ClassMap<RelatorioGrupoComplete>
    {
        public RelatorioGrupoCompleteMap()
        {
            Table("RELATORIOS_GRUPOS");
            Id(a => a.key, "ID_GRUPO");
            Map(a => a.value, "NOME");
        }
    }
}
