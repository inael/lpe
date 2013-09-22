using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Questionario : AuditoriaEntidadesBd
    {
        public virtual int IdQuestionario { get; set; }         //[ID_QUESTIONARIO]    NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual string NomeQuestionario { get; set; }    //[NOME]               NVARCHAR (50)  NOT NULL,
        public virtual string Descricao { get; set; }           //[DESCRICAO]          NVARCHAR (300) NULL,
        public virtual string Instrucao { get; set; }           //[INSTRUCAO]          TEXT           NOT NULL,
        //public virtual IList<Resultado> QuestionarioResultado { get; set; }
        //public virtual IList<Questao> QuestionarioQuestao { get; set; }
        //public virtual IList<UsuarioToQuestionario> QuestionarioUserQuestionario { get; set; }
        //public virtual IList<OpcaoRespostaToQuestionario> QuestionarioOpRespQuest { get; set; }
    }
}
