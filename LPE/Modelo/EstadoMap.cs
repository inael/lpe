using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Table("ESTADOS");
            Id(a => a.IdEstado, "ID_ESTADO");
            Map(a => a.UF, "SIGLA");
            Map(a => a.NomeEstado, "NOME");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.IdMunicipioEstado)
            //        .KeyColumn("ID_ESTADO")
            //        .Inverse()
            //        .LazyLoad();
        }           
    }
}
