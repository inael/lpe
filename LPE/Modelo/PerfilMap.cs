using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class PerfilMap : ClassMap<Perfil>
    {
        public PerfilMap()
        {
            Table("PERFIS");
            Id(a => a.IdPerfil, "ID_PERFIL");
            Map(a => a.Nome, "NOME");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.UsuarioPerfil)
            //    .KeyColumn("ID_PERFIL")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
