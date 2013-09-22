using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace AutoCompleteModel
{
    public class CBOSinonimoCompleteMap : ClassMap<CBOSinonimoComplete>
    {
        public CBOSinonimoCompleteMap()
        {
            Table("CBO_SINONIMOS");
            Id(a => a.key, "ID_SINONIMO");
            Map(a => a.value, "DESCRICAO");
        }
    }
}
