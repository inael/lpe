using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Modelo;

namespace AutoCompleteModel
{
    public class NivelEscolaridadeCompleteMap : ClassMap<NivelEscolaridadeComplete>
    {
        public NivelEscolaridadeCompleteMap()
        {
            Table("ESCOLARIDADE_NIVEL");
            Id(a => a.key, "ID_NIVEL_ESCOLARIDADE");
            Map(a => a.value, "DESCRICAO");
        }
    }
}
