using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class QuestaoMap : ClassMap<Questao>
    {
        public QuestaoMap()
        {
            Table("QUESTOES");
            Id(a => a.IdQuestao, "ID_QUESTAO");
            References(a => a.idGrupo, "ID_GRUPO").LazyLoad();
            Map(a => a.Numero, "NUMERO");
            Map(a => a.Enunciado, "ENUNCIADO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.idQuestaoToQuestionario)
            //    .KeyColumn("ID_QUESTAO")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
