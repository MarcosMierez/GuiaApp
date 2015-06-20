using System.Web.Mvc;
using GuiaPalestra.Helpers;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Areas.Cadastro.Controllers
{
    public class CoordenadorMController : Controller
    {
        private Usuario _usuario;

        public CoordenadorMController()
        {
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View(Construtor.CoordenadorApp().GetAll());
        }

        public ActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(Coordenador entidade)
        {
            if (ModelState.IsValid)
            {
                Construtor.CoordenadorApp().Save(entidade);
            return  RedirectToAction("Index");
            }
            return View(entidade);
        }

        public ActionResult Editar()
        {
            return View(ConvertVM(Construtor.CoordenadorApp().GetById(_usuario.ID)));
        }
        [HttpPost]
        public ActionResult Editar(CoordenadorVM entidade)
        {
            var coordenadorBD = Construtor.CoordenadorApp().GetById(entidade.ID);
            entidade.PhotoPath = coordenadorBD.Foto;
            if (ModelState.IsValid && coordenadorBD != null)
            {
                var coordenadorModel = ConvertModel(entidade);
                if (entidade.Photo != null)
                {
                    coordenadorModel.Foto = UploadPhoto.UploadPhotos(entidade.Photo, _usuario.Permissao[0], coordenadorBD.Foto);
                }

                Construtor.CoordenadorApp().Update(coordenadorModel);
                return RedirectToAction("Detalhe","CoordenadorM");
            }
            return View(entidade);
        }

        public ActionResult Detalhe()
        {
            return View(ConvertVM(Construtor.CoordenadorApp().GetById(_usuario.ID)));
        }
        public ActionResult PerfilCoordenador(string id)
        {
            return View(ConvertVM(Construtor.CoordenadorApp().GetById(id)));
        }
        private CoordenadorVM ConvertVM(Coordenador coordenador)
        {
            var tempC = new CoordenadorVM
            {
                ID = coordenador.ID,
                Email = coordenador.Email,
                Nome = coordenador.Nome,
                PhotoPath = coordenador.Foto

            };
            return tempC;
        }
        private Coordenador ConvertModel(CoordenadorVM coordeandor)
        {
            var tempC = new Coordenador
            {
                ID = coordeandor.ID,
                Email = coordeandor.Email,
                Nome = coordeandor.Nome,
                Foto = coordeandor.PhotoPath
            };
            return tempC;
        }
    }
}