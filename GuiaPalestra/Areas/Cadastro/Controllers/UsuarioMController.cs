using System.Web.Mvc;
using GuiaPalestra.Helpers;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Areas.Cadastro.Controllers
{
    public class UsuarioMController : Controller
    {
        private Usuario _usuario;

        public UsuarioMController()
        {
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Index(Usuario entidade)
        {
            if (ModelState.IsValid)
            {
               Construtor.UsuarioApp().Save(entidade);
                this.Flash("Registro efetuado com sucesso");
                return RedirectToAction("Index","Acesso",new{Area=""});
            }

            return View(entidade);
        }
        [Authorize(Roles = "Usuario")]
        public ActionResult Update()
        {
            return View(ConvertVM(Construtor.UsuarioApp().GetById(_usuario.ID)));
        }
        public ActionResult Detalhes()
        {
            return View(ConvertVM(Construtor.UsuarioApp().GetById(_usuario.ID)));
        }
        [Authorize(Roles = "Usuario")]
        [HttpPost]
        public ActionResult Update(UsuarioVM entidade)
        {
            var usuarioBD = Construtor.UsuarioApp().GetById(entidade.ID);
            entidade.PhotoPath = usuarioBD.Foto;
            if (ModelState.IsValid && usuarioBD != null)
            {
                var coordenadorModel = ConvertModel(entidade);
                if (entidade.Photo != null)
                {
                    
                    coordenadorModel.Foto = UploadPhoto.UploadPhotos(entidade.Photo, _usuario.Permissao[0], usuarioBD.Foto);
                    entidade.PhotoPath = coordenadorModel.Foto;
                }

                Construtor.UsuarioApp().Update(coordenadorModel);
                return RedirectToAction("Detalhes", "UsuarioM");
            }
            return View(entidade);

        }
        private UsuarioVM ConvertVM(Usuario usuario)
        {
            var U = new UsuarioVM
            {
                ID = usuario.ID,
                Email = usuario.Email,
                Nome = usuario.Nome,
                PhotoPath = usuario.Foto

            };
            return U;
        }
        private Usuario ConvertModel(UsuarioVM usuarioVm)
        {
            var tempU = new Coordenador
            {
                ID = usuarioVm.ID,
                Email = usuarioVm.Email,
                Nome = usuarioVm.Nome,
                Foto = usuarioVm.PhotoPath
            };
            return tempU;
        }



    }
}