using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class MunicipioMap : ClassMap<Municipio>
    {
        public MunicipioMap()
        {
            Table("MUNICIPIOS");
            Id(a => a.IdMunicipio, "ID_MUNICIPIO");
            References(a => a.IdEstadoMunicipio, "ID_ESTADO");
            Map(a => a.IBGE, "IBGE");
            Map(a => a.IDUF, "IDUF");
            Map(a => a.NomeMunicipio, "NOME");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
