using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class CBOSinonimosMap : ClassMap<CBOSinonimos>
    {
        public CBOSinonimosMap()
        {
            Table("CBO_SINONIMOS");
            Id(a => a.IdCBOSinonimo, "ID_SINONIMO");
            //References(a => a.idEndereco, "ID_ENDERECO").LazyLoad();
            Map(a => a.Descricao, "DESCRICAO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}
