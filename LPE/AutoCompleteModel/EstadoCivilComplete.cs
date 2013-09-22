using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCompleteModel
{
    [Serializable]
    public class EstadoCivilComplete
    {
        public virtual string key { get; set; }    //[DECRICAO]                VARCHAR (60)  NOT NULL,
        public virtual string value { get; set; }  //[ID_ESTADO_CIVIL]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    }
}
