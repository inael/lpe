using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class RelatorioGrupo
    {
        public virtual int IdGrupo { get; set; }                //[ID_GRUPO]          NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual string NomeGrupo { get; set; }           //[NOME]              NVARCHAR (200) NOT NULL,
        public virtual string UsuarioInclusao { get; set; }     //[USUARIO_INCLUSAO]   NVARCHAR (30)   NULL,
        public virtual DateTime DataInclusao { get; set; }      //[DATA_INCLUSAO]      DATETIME        NULL,
        public virtual string UsuarioAteracao { get; set; }     //[USUARIO_ALTERACAO]  NVARCHAR (30)   NULL,
        public virtual DateTime DataAteracao { get; set; }      //[DATA_ALTERACAO]     DATETIME        NULL,
        public virtual bool Excluido { get; set; }              //[EXCLUIDO]           NUMERIC (18)    NULL,
    }
}
