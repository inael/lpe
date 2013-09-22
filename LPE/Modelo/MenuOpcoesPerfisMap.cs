using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class MenuOpcoesPerfisMap : ClassMap<MenuOpcoesPerfis>
    {
        public MenuOpcoesPerfisMap()
        {
            Table("MENU_OPCOES_PERFIS");
            Id(a => a.IdMenuOpcoesPerfis, "ID_MENU_OPC_PER");
            References(a => a.MenuOpcIdMenuOpcPer, "ID_MENU_OPC");
            References(a => a.PerfilIdMenuOpcPer, "ID_PERFIL");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
