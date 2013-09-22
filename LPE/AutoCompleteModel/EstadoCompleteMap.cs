using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Modelo;

namespace AutoCompleteModel
{
    public class EstadoCompleteMap : ClassMap<EstadoComplete>
    {
        public EstadoCompleteMap()
        {
            Table("ESTADOS");
            Id(a => a.key, "ID_ESTADO");
            Map(a => a.value, "NOME");
        }
    }
}
