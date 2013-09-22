using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class OpcaoResposta : AuditoriaEntidadesBd
    {
        public virtual int IdOpcaoResposta { get; set; }        //[ID_OPCAO_RESPOSTA]  NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual string Nome { get; set; }                //[NOME]               NVARCHAR (50) NOT NULL,
        public virtual int Valor { get; set; }                  //[VALOR]              NUMERIC (18)  NOT NULL,
        //public virtual IList<OpcaoRespostaToQuestionario> idOpcoesRespostaToQuestionario { get; set; }
    }
}
