using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Modelo;

namespace AutoCompleteModel
{
    public class CityCompleteMap : ClassMap<CityComplete>
    {
        public CityCompleteMap()
        {
            Table("MUNICIPIOS");
            Id(a => a.key, "ID_MUNICIPIO");
            Map(a => a.value, "NOME");
        }
    }
}
