using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Municipio : AuditoriaEntidadesBd
    {
        public virtual int IdMunicipio { get; set; }            //[ID_MUNICIPIO]      NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
        public virtual Estado IdEstadoMunicipio { get; set; }   //[ID_ESTADO]         NUMERIC (18)  NOT NULL,
        public virtual string IBGE { get; set; }                //[IBGE]              CHAR (7)      NOT NULL,
        public virtual int IDUF { get; set; }                   //[IDUF]              INT           NOT NULL,
        public virtual string NomeMunicipio { get; set; }       //[NOME]              VARCHAR (60)  NULL,
    }
}
