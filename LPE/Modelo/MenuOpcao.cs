using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class MenuOpcao : AuditoriaEntidadesBd
    {
        public virtual int IdMenuOpc { get; set; }              //[ID_MENU_OPC]       NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual string NomeMen { get; set; }             //[NOME_MEN]          VARCHAR (60)  NOT NULL,
        public virtual string PastaMen { get; set; }            //[PASTA_MEN]         VARCHAR (60)  NOT NULL,
        public virtual string ControladorMen { get; set; }      //[CONTROLADOR_MEN]   VARCHAR (60)  NOT NULL,
        public virtual string AcaoMen { get; set; }             //[ACAO_MEN]          VARCHAR (60)  NOT NULL,
        public virtual string IconeMen { get; set; }            //[ICONE_MEN]         VARCHAR (60)  NOT NULL,
        public virtual IList<MenuOpcoesPerfis> IdMenuOpc_MenuOpcPer{ get; set; }
    }
}
