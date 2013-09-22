using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class Escolaridade : AuditoriaEntidadesBd
    {
        public virtual int IdEscolaridade { get; set; }                                 //[ID_ESCOLARIDADE]       NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual NivelEscolaridade IdNivelEscolaridadeEscolaridade { get; set; }  //[ID_NIVEL_ESCOLARIDADE] NUMERIC (18)  NOT NULL,
        public virtual string DescricaoEscolaridade { get; set; }                       //[DESCRICAO]             NVARCHAR (50) NOT NULL,
    }
}
