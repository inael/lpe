using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class NivelEscolaridadeMap : ClassMap<NivelEscolaridade>
    {
        public NivelEscolaridadeMap()
        {
            Table("ESCOLARIDADE_NIVEL");
            Id(a => a.IdNivelEscolaridade, "ID_NIVEL_ESCOLARIDADE");
            Map(a => a.DescricaoNivelEscolaridade, "DESCRICAO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.IdEscolaridadeNivel)
            //    .KeyColumn("ID_NIVEL_ESCOLARIDADE")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
