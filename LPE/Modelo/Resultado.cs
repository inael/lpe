using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class Resultado
    {
        public virtual int IdResultado { get; set; }                 //[ID_RESULTADO]       NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual Usuario IdUsuario { get; set; }               //[ID_USUARIO]         NUMERIC (18)  NOT NULL,
        public virtual Questionario IdQuestionario { get; set; }     //[ID_QUESTIONARIO]    NUMERIC (18)  NOT NULL,
        public virtual Relatorio IdRelatorio { get; set; }           //[ID_RELATORIO]       NUMERIC (18)  NOT NULL,
        public virtual double Valor { get; set; }                    //[VALOR]              NUMERIC (18)  NOT NULL,
        public virtual bool Pdf { get; set; }                        //[PDF]                BIT             NULL,
        public virtual string UsuarioInclusao { get; set; }          //[USUARIO_INCLUSAO]   NVARCHAR (30)   NULL,
        public virtual DateTime DataInclusao { get; set; }           //[DATA_INCLUSAO]      DATETIME        NULL,
        public virtual string UsuarioAteracao { get; set; }          //[USUARIO_ALTERACAO]  NVARCHAR (30)   NULL,
        public virtual DateTime DataAteracao { get; set; }           //[DATA_ALTERACAO]     DATETIME        NULL,
        public virtual bool Excluido { get; set; }                   //[EXCLUIDO]           NUMERIC (18)    NULL,
    }
}
