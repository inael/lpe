using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class RelatorioMap : ClassMap<Relatorio>
    {
        public RelatorioMap()
        {
            Table("RELATORIOS");
            Id(a => a.IdRelatorio, "ID_RELATORIO");
            References(a => a.IdGrupo, "ID_GRUPO");
            Map(a => a.IdNivel,"ID_NIVEL");
            Map(a => a.Caracteristica,"CARACTERISTICA");
            Map(a => a.Motiva,"MOTIVA");
            Map(a => a.Desagrada,"DESAGRADA");
            Map(a => a.Potencial,"POTENCIAL");
            Map(a => a.ValorMin,"VALOR_MIN");
            Map(a => a.ValorMax, "VALOR_MAX");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.LaudoResultado)
            //    .KeyColumn("ID_LAUDO")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
