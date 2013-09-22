using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class QuestaoGrupoMap : ClassMap<QuestaoGrupo>
    {
        public QuestaoGrupoMap()
        {
            Table("QUESTOES_GRUPOS");
            Id(a => a.IdGrupo, "ID_GRUPO");
            Map(a => a.Nome, "NOME");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
