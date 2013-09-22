using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class QuestionarioMap : ClassMap<Questionario>
    {
        public QuestionarioMap()
        {
            Table("QUESTIONARIOS");
            Id(a => a.IdQuestionario, "ID_QUESTIONARIO");
            Map(a => a.NomeQuestionario, "NOME");
            Map(a => a.Descricao, "DESCRICAO");
            Map(a => a.Instrucao, "INSTRUCAO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.QuestionarioResultado)
            //    .KeyColumn("ID_QUESTIONARIO")
            //    .Inverse()
            //    .LazyLoad();
            //HasMany(a => a.QuestionarioQuestao)
            //    .KeyColumn("ID_QUESTIONARIO")
            //    .Inverse()
            //    .LazyLoad();
            //HasMany(a => a.QuestionarioUserQuestionario)
            //    .KeyColumn("ID_QUESTIONARIO")
            //    .Inverse()
            //    .LazyLoad();
            //HasMany(a => a.QuestionarioOpRespQuest)
            //    .KeyColumn("ID_QUESTIONARIO")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
