using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class UsuarioToQuestionarioMap : ClassMap<UsuarioToQuestionario>
    {
        public UsuarioToQuestionarioMap()
        {
            Table("USUARIOS_QUESTIONARIOS");
            Id(a => a.IdUsuarioQuestionario, "ID_USUARIO_QUESTIONARIO");
            References(a => a.idUsuario, "ID_USUARIO").LazyLoad();
            References(a => a.idQuestionario, "ID_QUESTIONARIO").LazyLoad();
            Map(a => a.Concluido, "CONCLUIDO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
        }
    }
}