using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCompleteModel
{
    [Serializable]
    public class EscolaridadeComplete
    {
        public virtual string key { get; set; }    //[DESCRICAO]          VARCHAR (60)  NOT NULL,
        public virtual string value { get; set; }  //[ID_ESCOLARIDADE]    NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    }
}
