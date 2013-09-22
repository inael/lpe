using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("USUARIOS");
            Id(a => a.IdUsuario, "ID_USUARIO");
            References(a => a.Pessoa_Usuario, "ID_PESSOA").LazyLoad();
            References(a => a.Perfil_Usuario, "ID_PERFIL").LazyLoad();
            Map(a => a.Login, "LOGIN");
            Map(a => a.Senha, "SENHA");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.UsuarioResposta)
            //    .KeyColumn("ID_USUARIO")
            //    .Inverse()
            //    .LazyLoad();
            //HasMany(a => a.UsuarioResultado)
            //    .KeyColumn("ID_USUARIO")
            //    .Inverse()
            //    .LazyLoad();
            //HasMany(a => a.UsuarioUsuarioQuestionario)
            //    .KeyColumn("ID_USUARIO")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}