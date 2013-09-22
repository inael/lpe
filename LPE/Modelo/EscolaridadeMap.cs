using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class EscolaridadeMap : ClassMap<Escolaridade>
    {
        public EscolaridadeMap()
        {
            Table("ESCOLARIDADE");
            Id(a => a.IdEscolaridade, "ID_ESCOLARIDADE");
            References(a => a.IdNivelEscolaridadeEscolaridade, "ID_NIVEL_ESCOLARIDADE");
            Map(a => a.DescricaoEscolaridade, "DESCRICAO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
