using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class ResultadoMap : ClassMap<Resultado>
    {
        public ResultadoMap()
        {
            Table("RESULTADOS");
            Id(a => a.IdResultado, "ID_RESULTADO");
            References(a => a.IdUsuario, "ID_USUARIO").LazyLoad();
            References(a => a.IdQuestionario, "ID_QUESTIONARIO").LazyLoad();
            References(a => a.IdRelatorio, "ID_RELATORIO").LazyLoad();
            Map(a => a.Valor, "VALOR");
            Map(a => a.Pdf, "PDF");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
