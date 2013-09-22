using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class RespostaMap : ClassMap<Resposta>
    {
        public RespostaMap()
        {
            Table("RESPOSTAS");
            Id(a => a.IdResposta, "ID_RESPOSTA");
            References(a => a.idUsuario, "ID_USUARIO").LazyLoad();
            References(a => a.idQuestaoToQuestionario, "ID_QUESTAO_QUESTIONARIO").LazyLoad();
            References(a => a.IdOpcaoRespostaQuestionario, "ID_OPCAO_RESPOSTA_QUESTIONARIO").LazyLoad();
            Map(a => a.DataResposta, "DATA_RESPOSTA");
            Map(a => a.HoraReposta, "HORA_RESPOSTA");
        }
    }
}
