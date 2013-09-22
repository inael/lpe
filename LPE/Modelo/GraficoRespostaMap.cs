using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class GraficoRespostaMap : ClassMap<GraficoResposta>
    {
        public GraficoRespostaMap()
        {
            Id(a => a.IdGrafico, "ID_GRUPO");
            Map(a => a.Perfil, "Perfil");
            //Map(a => a.Inferior, "Inferior");
            //Map(a => a.MedioInferior, "MedioInferior");
            //Map(a => a.Medio, "Medio");
            //Map(a => a.MedioSuperior, "MedioSuperior");
            //Map(a => a.SuperiorAMedio, "SuperiorAMedio");
            //Map(a => a.Superior, "Superior");
            Map(a => a.ValorRespostas, "Respostas");
        }
    }
}
