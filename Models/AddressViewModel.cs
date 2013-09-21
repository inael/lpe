using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewWebMvc.Models
{
    public class AddressViewModel
    {
        public int idEndereco { get; set; }
        public string idMunicipio { get; set; }
        public string selectMunicipio { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
    }
}