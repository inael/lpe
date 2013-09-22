using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class OpcaoRespostaToQuestionario : AuditoriaEntidadesBd
    {
        public virtual int IdOpcaoRespostaQuestionario { get; set; }     //[ID_OPCAO_RESPOSTA_QUESTIONARIO] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual OpcaoResposta idOpcaoResposta { get; set; }       //[ID_OPCAO_RESPOSTA]              NUMERIC (18)  NOT NULL,
        public virtual Questionario idQuestionario { get; set; }         //[ID_QUESTIONARIO]                NUMERIC (18)  NOT NULL,       
        //public virtual IList<Resposta> idResposta { get; set; }
    }
}
