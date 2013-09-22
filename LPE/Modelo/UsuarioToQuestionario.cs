using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class UsuarioToQuestionario : AuditoriaEntidadesBd
    {
        public virtual int IdUsuarioQuestionario { get; set; }      //[ID_USUARIO_QUESTIONARIO] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual Usuario idUsuario { get; set; }              //[ID_USUARIO]              NUMERIC (18)  NOT NULL,
        public virtual Questionario idQuestionario { get; set; }    //[ID_QUESTIONARIO]         NUMERIC (18)  NOT NULL,
        public virtual bool Concluido { get; set; }                 //[CONCLUIDO]               INT    NULL,
    }
}
