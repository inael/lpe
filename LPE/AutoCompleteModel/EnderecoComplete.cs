using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCompleteModel
{
    [Serializable]
    public class EnderecoComplete
    {
        public virtual string key { get; set; }    //[LOGRADOURO]          VARCHAR (60)  NOT NULL,
        public virtual string value { get; set; }  //[ID_ENDERECO]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    }
}
