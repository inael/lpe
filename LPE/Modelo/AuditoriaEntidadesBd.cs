using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    public class AuditoriaEntidadesBd
    {
        public virtual string UsuarioInclusao { get; set; }               //[USUARIO_INCLUSAO]   NVARCHAR (30)   NULL,
        public virtual DateTime DataInclusao { get; set; }                //[DATA_INCLUSAO]      DATETIME        NULL,
        public virtual string UsuarioAteracao { get; set; }               //[USUARIO_ALTERACAO]  NVARCHAR (30)   NULL,
        public virtual DateTime DataAteracao { get; set; }                //[DATA_ALTERACAO]     DATETIME        NULL,
        public virtual bool Excluido { get; set; }                        //[EXCLUIDO]           NUMERIC (18)    NULL,
    }
}
