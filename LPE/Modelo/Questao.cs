using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Questao : AuditoriaEntidadesBd
    {
        public virtual int IdQuestao { get; set; }                      //[ID_QUESTAO]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual QuestaoGrupo idGrupo { get; set; }               //[ID_GRUPO]          NUMERIC (18)  NOT NULL,
        public virtual int Numero { get; set; }                         //[NUMERO]             NUMERIC (18)  NULL,
        public virtual string Enunciado { get; set; }                   //[ENUNCIADO]          TEXT          NOT NULL,
        //public virtual IList<QuestaoToQuestionario> idQuestaoToQuestionario { get; set; }
    }
}
