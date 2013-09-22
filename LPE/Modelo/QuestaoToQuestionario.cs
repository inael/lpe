using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class QuestaoToQuestionario : AuditoriaEntidadesBd
    {
        public virtual int idQuestaoToQuestionario { get; set; }    //[ID_QUESTAO_QUESTIONARIO] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual Questao idQuestao { get; set; }              //[ID_QUESTAO]              NUMERIC (18)  NOT NULL,
        public virtual Questionario idQuestionario { get; set; }    //[ID_QUESTIONARIO]         NUMERIC (18)  NOT NULL,
        //public virtual IList<Resposta> idResposta { get; set; }
    }
}
