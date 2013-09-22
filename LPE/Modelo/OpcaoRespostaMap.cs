using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class OpcaoRespostaMap : ClassMap<OpcaoResposta>
    {
        public OpcaoRespostaMap()
        {
            Table("OPCOES_RESPOSTA");
            Id(a => a.IdOpcaoResposta, "ID_OPCAO_RESPOSTA");
            Map(a => a.Nome, "NOME");
            Map(a => a.Valor, "VALOR");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.idOpcoesRespostaToQuestionario)
            //    .KeyColumn("ID_OPCAO_RESPOSTA")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}