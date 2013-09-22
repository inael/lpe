using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class EstadoCivil : AuditoriaEntidadesBd
    {
        public virtual int IdEstadoCivil { get; set; }  //[ID_ESTADO_CIVIL]   NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual string Descricao { get; set; }   //[DESCRICAO]         NVARCHAR (50) NOT NULL,
    }
}
