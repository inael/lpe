using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace AutoCompleteModel
{
    public class EscolaridadeCompleteMap : ClassMap<EscolaridadeComplete>
    {
        public EscolaridadeCompleteMap()
        {
            Table("ESCOLARIDADE");
            Id(a => a.key, "ID_ESCOLARIDADE");
            Map(a => a.value, "DESCRICAO");
        }
    }
}
