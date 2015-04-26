using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestra.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : Controller
    {
        private readonly Usuario _usuario;
        public UsuarioController()
        {
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventosDisponiveis()
        {
            
            return View(Construtor.EventoApp().EventosDisponiveis(_usuario.Permissao[0]));
        }

        public ActionResult PalestrasEvento(string id)
        {
            return View(Construtor.EventoApp().PalestrasDisponiveisEvento(id));
        }
        public ActionResult MinhasPalestras()
        {
            return View();
        }

        public ActionResult Inscrever(string id, string eventoId)
        {
            Construtor.EventoApp().InscreverPalestraEvento(id, eventoId);
            return RedirectToAction("PalestraEvento");
        }
  
    }
}