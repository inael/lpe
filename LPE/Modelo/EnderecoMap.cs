using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Table("ENDERECOS");
            Id(a => a.IdEndereco, "ID_ENDERECO");
            References(a => a.IdMunicipioEndereco, "ID_MUNICIPIO");
            Map(a => a.Logradouro, "LOGRADOURO");
            Map(a => a.Bairro, "BAIRRO");
            Map(a => a.Cep, "CEP");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.idPessoaEndereco)
            //    .KeyColumn("ID_ENDERECO")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
