using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewWebMvc.Models
{
    public class EscolaridadeViewModel
    {
        public int idEscolaridade { get; set; }
        public string idNivelEscolaridade { get; set; }
        public string selectNivel { get; set; }
        public string DescricaoEscolaridade { get; set; }
    }
}