using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Modelo;

namespace AutoCompleteModel
{
    public class EstadoCivilCompleteMap : ClassMap<EstadoCivilComplete>
    {
        public EstadoCivilCompleteMap()
        {
            Table("ESTADO_CIVIL");
            Id(a => a.key, "ID_ESTADO_CIVIL");
            Map(a => a.value, "DESCRICAO");
        }
    }
}
