using System.Web;
using System.Web.Mvc;
using GuiaPalestra.Helpers;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Areas.Cadastro.Controllers
{
    public class PalestranteController : Controller
    {
        private Usuario _usuario;

        public PalestranteController()
        {
            _usuario = Seguranca.Usuario();
        }

        public ActionResult Cadastrar()
        {
            return View(new Palestrante());
        }
        [HttpPost]
        public ActionResult Cadastrar(Palestrante entidade)
        {
            if (ModelState.IsValid)
            {
                Construtor.PalestranteApp().Save(entidade);
                return RedirectToAction("Index");
            }
            return View(entidade);

        }
        public ActionResult Detalhe()
        {
            return View(ConvertVM(Construtor.PalestranteApp().Details(_usuario.ID)));
        }

        public ActionResult Editar()
        {
            return View(ConvertVM(Construtor.PalestranteApp().Details(_usuario.ID)));
        }
        [HttpPost]
        public ActionResult Editar(PalestranteVM entidade)
        {
            var palestranteBD = Construtor.PalestranteApp().Details(entidade.ID);
            entidade.PhotoPath = palestranteBD.Foto;
            if (ModelState.IsValid && palestranteBD != null)
            {
                var palestranteModel = ConvertModel(entidade);
                if (entidade.Photo!=null)
                {
                    palestranteModel.Foto = UploadPhoto.UploadPhotos(entidade.Photo, _usuario.Permissao[0], palestranteBD.Foto);
                }
                
                Construtor.PalestranteApp().Edit(palestranteModel);
                return RedirectToAction("Detalhe","Palestrante");
            }
            return View(entidade);

        }

        public ActionResult Delete(string id)
        {
            Construtor.PalestranteApp().Remove(id);
            return RedirectToAction("Index");
        }

        private PalestranteVM ConvertVM(Palestrante palestrante)
        {
            var tempPalestrante = new PalestranteVM
            {
                ID = palestrante.ID,
                Email = palestrante.Email,
                Nome = palestrante.Nome,
                TwitterPalestrante = palestrante.TwitterPalestrante,
                PhotoPath =palestrante.Foto

            };
            return tempPalestrante;
        }
        private Palestrante ConvertModel(PalestranteVM palestrante)
        {
            var tempPalestrante = new Palestrante
            {
                ID = palestrante.ID,
                Email = palestrante.Email,
                Nome = palestrante.Nome,
                TwitterPalestrante = palestrante.TwitterPalestrante,
                Foto = palestrante.PhotoPath
            };
            return tempPalestrante;
        }



    }
}