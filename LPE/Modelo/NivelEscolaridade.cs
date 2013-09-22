using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class NivelEscolaridade : AuditoriaEntidadesBd
    {
        public virtual int IdNivelEscolaridade { get; set; }            //[ID_NIVEL_ESCOLARIDADE] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual string DescricaoNivelEscolaridade { get; set; }  //[DESCRICAO]             NVARCHAR (50) NOT NULL,

        //public virtual IList<Escolaridade> IdEscolaridadeNivel { get; set; }
    }
}
