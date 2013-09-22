using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Relatorio
    {
        public virtual int IdRelatorio { get; set; }            //[ID_RELATORIO]           NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual RelatorioGrupo IdGrupo { get; set; }       //[ID_GRUPO]               NVARCHAR (20) NOT NULL,
        public virtual int IdNivel { get; set; }                //[DESCRICAO]          TEXT          NOT NULL,
        public virtual string Caracteristica { get; set; }      //[CARACTERISTICA]    TEXT          NULL,
        public virtual string Motiva { get; set; }              //[MOTIVA]            TEXT          NULL,
        public virtual string Desagrada { get; set; }           //[DESAGRADA]         TEXT          NULL,
        public virtual string Potencial { get; set; }           //[POTENCIAL]         TEXT          NULL,
        public virtual double ValorMin { get; set; }            //[VALOR_MIN]         NUMERIC (18)  NULL,
        public virtual double ValorMax { get; set; }            //[VALOR_MAX]         NUMERIC (18)  NULL,
        public virtual string UsuarioInclusao { get; set; }     //[USUARIO_INCLUSAO]   NVARCHAR (30)   NULL,
        public virtual DateTime DataInclusao { get; set; }      //[DATA_INCLUSAO]      DATETIME        NULL,
        public virtual string UsuarioAteracao { get; set; }     //[USUARIO_ALTERACAO]  NVARCHAR (30)   NULL,
        public virtual DateTime DataAteracao { get; set; }      //[DATA_ALTERACAO]     DATETIME        NULL,
        public virtual bool Excluido { get; set; }              //[EXCLUIDO]           NUMERIC (18)    NULL,
        //public virtual IList<Resultado> LaudoResultado { get; set; }
    }
}
