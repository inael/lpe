using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class CBOSinonimos : AuditoriaEntidadesBd
    {
        public virtual int IdCBOSinonimo { get; set; }   //[ID_SINONIMO]       NUMERIC (18)   NOT NULL,
        //public virtual string IdPessoa { get; set; }   //[ID_OCUPACAO]       NUMERIC (18)   NOT NULL,
        public virtual string Descricao { get; set; }    //[DESCRICAO]         NVARCHAR (300) NOT NULL,
    }
}
