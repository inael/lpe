using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    public class GraficoResposta
    {
        public virtual int IdGrafico { get; set; }               
        public virtual string Perfil { get; set; }               
        //public virtual double Inferior { get; set; }
        //public virtual double MedioInferior { get; set; }
        //public virtual double Medio { get; set; }
        //public virtual double MedioSuperior { get; set; }
        //public virtual double SuperiorAMedio { get; set; }
        //public virtual double Superior { get; set; }
        public virtual double ValorRespostas { get; set; }             
    }
}
