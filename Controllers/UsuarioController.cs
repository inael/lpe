using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Modelo;
using System.Collections.Specialized;

namespace ViewWebMvc.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioBll negocio;

        public UsuarioController()
        {
            negocio = new UsuarioBll();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Usuario> lista = negocio.Listar();
            return View(lista);
        }

        public ActionResult IndexSelect()
        {
            List<Usuario> lista = negocio.ListarAtivos();
            return View(lista);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Usuario entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        //Nome, id_candidato (chave única), hash (MD5) da senha 
        public string CreateMPE(String nome, string IdCandidato, String SenhaHash, string Email)
        {
            try
            {
                int idPerfilCandidatoMPE = 3; //Id tabela Perfis referente ao perfil de acesso para o Candidato(Usuario) do sistema MPE

                PessoaBll bllPessoa = new PessoaBll();
                Pessoa returnPessoa = bllPessoa.Inserir(new Pessoa { NomePessoa = nome, EmailPessoa = Email });

                Usuario entidade = new Usuario
                {
                    Login = Email,
                    Senha = SenhaHash,
                    Perfil_Usuario = new Perfil { IdPerfil = idPerfilCandidatoMPE },
                    Pessoa_Usuario = new Pessoa { IdPessoa = returnPessoa.IdPessoa }
                };

                entidade.UsuarioInclusao = User.Identity.Name;
                negocio.Inserir(entidade);

                return "{'msg':'Sucess'}";
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível inserir o Candidato.",e);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection, Usuario entidade)
        {
            try
            {
                /*int idEmpresa = Convert.ToInt32(collection["empresa"]);
                entidade.EmpresaUsuario = new Empresa { Id = idEmpresa };

                int idMenuGrupo = Convert.ToInt32(collection["menuGrupo"]);
                entidade.MenuGrupoUsuario = new MenuGrupo { Id = idMenuGrupo };*/

                int idPessoa = Convert.ToInt32(collection["pessoa"]);
                entidade.Pessoa_Usuario = new Pessoa { IdPessoa = idPessoa };

                int idPerfil = Convert.ToInt32(collection["perfil"]);
                entidade.Perfil_Usuario = new Perfil { IdPerfil = idPerfil };

                entidade.UsuarioInclusao = User.Identity.Name;
                negocio.Inserir(entidade);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(entidade);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Usuario entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection, Usuario entidade)
        {
            try
            {
                entidade.UsuarioAteracao = entidade.UsuarioAteracao = User.Identity.Name;
                negocio.Alterar(entidade);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Usuario entidade = negocio.Consultar(id);
            return View(entidade);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
