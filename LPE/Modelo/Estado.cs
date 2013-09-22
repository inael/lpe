using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class Estado : AuditoriaEntidadesBd
    {
        public virtual int IdEstado { get; set; }         //[ID_ESTADO]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual string UF { get; set; }            //[SIGLA]             CHAR (2)      NOT NULL,
        public virtual string NomeEstado { get; set; }    //[NOME]              VARCHAR (60)  NOT NULL,

        //public virtual IList<Municipio> IdMunicipioEstado { get; set; }
    }
}
