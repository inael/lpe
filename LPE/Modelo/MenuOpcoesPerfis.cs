using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class MenuOpcoesPerfis : AuditoriaEntidadesBd
    {
        public virtual int IdMenuOpcoesPerfis{ get; set; }          //[ID_MENU_OPC_PER]   NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual MenuOpcao MenuOpcIdMenuOpcPer { get; set; }  //[ID_MENU_OPC]       NUMERIC (18)  NOT NULL,
        public virtual Perfil PerfilIdMenuOpcPer { get; set; }      //[ID_PERFIL]         NUMERIC (18)  NOT NULL,
    }
}
