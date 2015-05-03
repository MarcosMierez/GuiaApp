using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GuiaPalestra.Models;
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
        public static string _eventoId;
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
            _eventoId = id;
            return View(Construtor.EventoApp().PalestrasDisponiveisEvento(id,_usuario.ID));
        }
        public ActionResult EventosUsuario()
        {
            return View(Construtor.EventoApp().EventosUsuario(_usuario.ID));
        }
        public ActionResult MinhasPalestras(string id)
        {
            _eventoId = id;
            return View(Construtor.EventoApp().PalestrasAdicionadas(id,_usuario.ID));
        }
        public ActionResult Inscrever(string id, string eventoId,bool status,string trilhaId,string salaId)
        {
            Construtor.EventoApp().InscreverPalestraEvento(id, eventoId,_usuario.ID,status,trilhaId,salaId);
            return RedirectToAction("PalestrasEvento/"+_eventoId,"Usuario");
        }

        public ActionResult ConfirmarPresenca(string id,string palestraId)
        {
            Construtor.EventoApp().ConfirmarParticipacaoUsuario(id, palestraId, _usuario.ID);
            return RedirectToAction("MinhasPalestras/" + _eventoId, "Usuario");
        }

        public ActionResult RelatorioDePalestras()
        {
            return View();
        }

  
    }
}