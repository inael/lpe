using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Pessoa : AuditoriaEntidadesBd
    {
        public virtual int IdPessoa { get; set; }                   //[ID_PESSOA]          NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual Endereco idEndereco { get; set; }            //[ID_ENDERECO]        NUMERIC (18)   NOT NULL,
        public virtual Escolaridade IdEscolaridade { get; set;}     //[ID_ESCOLARIDADE]    NUMERIC (18)   NOT NULL,
        public virtual CBOSinonimos IdProfissao { get; set; }       //[ID_PROFISSAO]       NUMERIC (18)   NULL,
        public virtual EstadoCivil IdEstadoCivil { get; set; }      //[ID_ESTADO_CIVIL]    NUMERIC (18)   NOT NULL,
        public virtual string NomePessoa { get; set; }              //[NOME_PESSOA]        NVARCHAR (50)   NULL,
        public virtual string SobrenomePessoa { get; set; }         //[SOBRENOME_PESSOA]   NVARCHAR (80)   NULL,
        public virtual string TratamentoPessoa { get; set; }        //[TRATAMENTO_PESSOA]  NVARCHAR (4)    NULL,
        public virtual string EmailPessoa { get; set; }             //[EMAIL_PESSOA]       NVARCHAR (256)  NULL,
        public virtual string CPFPessoa { get; set; }               //[CPF]                CHAR (14)      NOT NULL,
        public virtual int IdadePessoa { get; set; }                //[IDADE]              INT            NOT NULL,
        public virtual DateTime? DtNascimentoPessoa { get; set; }   //[DATA_NASCIMENTO]    DATE           NOT NULL,
        public virtual char SexoPessoa { get; set; }                //[SEXO]               CHAR (1)       NOT NULL,
        
        //public virtual IList<Usuario> idUsuario { get; set; }
    }
}
