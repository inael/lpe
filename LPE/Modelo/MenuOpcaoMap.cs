using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class MenuOpcaoMap : ClassMap<MenuOpcao>
    {
        public MenuOpcaoMap()
        {
            Table("MENU_OPCOES");
            Id(a => a.IdMenuOpc, "ID_MENU_OPC");
            Map(a => a.NomeMen, "NOME_MEN");
            Map(a => a.PastaMen, "PASTA_MEN");
            Map(a => a.ControladorMen, "CONTROLADOR_MEN");
            Map(a => a.AcaoMen, "ACAO_MEN");
            Map(a => a.IconeMen, "ICONE_MEN");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            HasMany(a => a.IdMenuOpc_MenuOpcPer)
                .KeyColumn("ID_MENU_OPC")
                .Inverse()
                .LazyLoad();
        }
    }
}
