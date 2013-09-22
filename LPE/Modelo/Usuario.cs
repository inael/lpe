/*
 * Modelo da entidade Usuario
 * Arquiteto: José Lino Neto
 * Desenvolvedor: Thiago Zaidem
 * 
 * Escopo de validação:
 * O nome, login e senha são obrigatórios.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    [Serializable]
    public class Usuario : AuditoriaEntidadesBd
    {
        public virtual int IdUsuario { get; set; }                     //[ID_USUARIO]          NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
        public virtual Pessoa Pessoa_Usuario { get; set; }             //[ID_PESSOA]           NUMERIC (18)   NOT NULL,
        public virtual Perfil Perfil_Usuario { get; set; }             //[ID_PERFIL]           NUMERIC (18)   NOT NULL,
        [Required(ErrorMessage = "Digite um nome de usuário")]
        public virtual string Login { get; set; }                      //[LOGIN]              NVARCHAR (50)  NOT NULL,
        [Required(ErrorMessage = "Digite uma senha")]
        public virtual string Senha { get; set; }                      //[SENHA]              NVARCHAR (200) NOT NULL,
        //public virtual IList<Resposta> UsuarioResposta { get; set; }
        //public virtual IList<Resultado> UsuarioResultado { get; set; }
        //public virtual IList<UsuarioToQuestionario> UsuarioUsuarioQuestionario { get; set; }
     }
}