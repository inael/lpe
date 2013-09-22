using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace AutoCompleteModel
{
    public class EnderecoCompleteMap : ClassMap<EnderecoComplete>
    {
        public EnderecoCompleteMap()
        {
            Table("ENDERECOS");
            Id(a => a.key, "ID_ENDERECO");
            Map(a => a.value, "LOGRADOURO");
        }
    }
}
