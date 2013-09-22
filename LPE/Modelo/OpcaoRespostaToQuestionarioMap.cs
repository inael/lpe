using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class OpcaoRespostaToQuestionarioMap : ClassMap<OpcaoRespostaToQuestionario>
    {
        public OpcaoRespostaToQuestionarioMap()
        {
            Table("OPCOES_RESPOSTA_QUESTIONARIOS");
            Id(a => a.IdOpcaoRespostaQuestionario, "ID_OPCAO_RESPOSTA_QUESTIONARIO");
            References(a => a.idOpcaoResposta, "ID_OPCAO_RESPOSTA").LazyLoad();
            References(a => a.idQuestionario, "ID_QUESTIONARIO").LazyLoad();
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.idResposta)
            //        .KeyColumn("ID_OPCAO_RESPOSTA_QUESTIONARIO")
            //        .Inverse()
            //        .LazyLoad();
        }
    }
}
