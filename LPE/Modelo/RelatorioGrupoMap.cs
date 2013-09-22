using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class RelatorioGrupoMap : ClassMap<RelatorioGrupo>
    {
        public RelatorioGrupoMap()
        {
            Table("QUESTOES_GRUPOS");
            Id(a => a.IdGrupo, "ID_GRUPO");
            Map(a => a.NomeGrupo, "NOME");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
