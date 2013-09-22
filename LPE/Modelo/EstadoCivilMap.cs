using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class EstadoCivilMap : ClassMap<EstadoCivil>
    {
        public EstadoCivilMap()
        {
            Table("ESTADO_CIVIL");
            Id(a => a.IdEstadoCivil, "ID_ESTADO_CIVIL");
            Map(a => a.Descricao, "DESCRICAO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
