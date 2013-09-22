using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Resposta 
    {
        public virtual int IdResposta { get; set; }                                             //[ID_RESPOSTA]       NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual Usuario idUsuario { get; set; }                                          //[ID_USUARIO]        NUMERIC (18)  NOT NULL,
        public virtual QuestaoToQuestionario idQuestaoToQuestionario  { get; set; }             //[ID_QUESTAO_QUESTIONARIO]        NUMERIC (18)  NOT NULL,
        public virtual OpcaoRespostaToQuestionario IdOpcaoRespostaQuestionario { get; set; }    //[ID_OPCAO_RESPOSTA_QUESTIONARIO] NUMERIC (18)  NOT NULL,
        public virtual DateTime DataResposta { get; set; }                                      //[DATA_RESPOSTA]                  DATE         NOT NULL,
        public virtual DateTime HoraReposta { get; set; }                                       //[HORA_RESPOSTA]                  TIME (7)     NOT NULL
    }
}
