using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class EnderecoComplemento : AuditoriaEntidadesBd
    {
        public virtual int IdComplemento { get; set; }                  //[ID_COMPLEMENTO]    NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual Endereco IdEnderecoComplemento { get; set; }     //[ID_ENDERECO]       NUMERIC (18)   NOT NULL,
        public virtual string DescricaoComplemento { get; set; }        //[DESCRICAO]         NVARCHAR (300) NOT NULL,
    }
}
