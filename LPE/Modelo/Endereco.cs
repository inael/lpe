using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Endereco : AuditoriaEntidadesBd
    {
        public virtual int IdEndereco { get; set; }                 //[ID_ENDERECO]        NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual Municipio IdMunicipioEndereco { get; set; }  //[ID_MUNICIPIO]       NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual string Logradouro { get; set; }              //[LOGRADOURO]         NVARCHAR (100) NOT NULL,
        public virtual string Bairro { get; set; }                  //[BAIRRO]             NVARCHAR (200) NOT NULL,
        public virtual string Cep { get; set; }                     //[CEP]                NVARCHAR (15)  NOT NULL,

        public virtual string Municipio { get; set; }
        //public virtual IList<Pessoa> idPessoaEndereco { get; set; }
    }
}
