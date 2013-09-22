using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class QuestaoToQuestionarioMap : ClassMap<QuestaoToQuestionario>
    {
        public QuestaoToQuestionarioMap()
        {
            Table("QUESTOES_QUESTIONARIOS");
            Id(a => a.idQuestaoToQuestionario, "ID_QUESTAO_QUESTIONARIO");
            References(a => a.idQuestao, "ID_QUESTAO").LazyLoad();
            References(a => a.idQuestionario, "ID_QUESTIONARIO").LazyLoad();
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.idResposta)
            //       .KeyColumn("ID_QUESTAO_QUESTIONARIO")
            //       .Inverse()
            //       .LazyLoad();
        }
    }
}
