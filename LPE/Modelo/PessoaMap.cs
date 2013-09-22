using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Modelo
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Table("PESSOAS");
            Id(a => a.IdPessoa, "ID_PESSOA");
            References(a => a.idEndereco, "ID_ENDERECO").LazyLoad();
            References(a => a.IdEscolaridade ,"ID_ESCOLARIDADE").LazyLoad();
            References(a => a.IdProfissao,"ID_SINONIMO").LazyLoad();
            References(a => a.IdEstadoCivil, "ID_ESTADO_CIVIL").LazyLoad();
            Map(a => a.NomePessoa, "NOME_PESSOA");
            Map(a => a.SobrenomePessoa, "SOBRENOME_PESSOA");
            Map(a => a.TratamentoPessoa, "TRATAMENTO_PESSOA");
            Map(a => a.EmailPessoa, "EMAIL_PESSOA");
            Map(a => a.CPFPessoa, "CPF");
            Map(a => a.IdadePessoa, "IDADE");
            Map(a => a.DtNascimentoPessoa, "DATA_NASCIMENTO");
            Map(a => a.SexoPessoa, "SEXO");
            Map(a => a.UsuarioInclusao, "USUARIO_INCLUSAO");
            Map(a => a.DataInclusao, "DATA_INCLUSAO");
            Map(a => a.UsuarioAteracao, "USUARIO_ALTERACAO");
            Map(a => a.DataAteracao, "DATA_ALTERACAO");
            Map(a => a.Excluido, "EXCLUIDO");
            //HasMany(a => a.idUsuario)
            //    .KeyColumn("ID_PESSOA")
            //    .Inverse()
            //    .LazyLoad();
        }
    }
}
