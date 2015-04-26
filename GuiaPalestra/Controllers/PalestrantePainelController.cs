using System.Web.Mvc;
using System.Web.UI.WebControls;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Controllers
{
    [Authorize(Roles = "Palestrante")]
    public class PalestrantePainelController : Controller
    {
        private readonly Usuario _usuario;
        public static string _eventoId;

        public PalestrantePainelController()
        {
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CriarPalestras()
        {
            return View(new Palestra());
        }
        [HttpPost]
        public ActionResult CriarPalestras(Palestra entidade)
        {
            entidade.PalestranteId = _usuario.ID;
            Construtor.PalestraApp().Save(entidade);
            return RedirectToAction("Index");
        }

        public ActionResult MinhasPalestras()
        {

            return View(Construtor.PalestraApp().TodasPalestrantePalestras(_usuario.ID));
        }

        public ActionResult EditarPalestra(string id)
        {
            return View(Construtor.PalestraApp().ListarPalestrasDoPalestrantePorId(id, _usuario.ID));
        }
        [HttpPost]
        public ActionResult EditarPalestra(Palestra entidade)
        {
            Construtor.PalestraApp().AlterarPalestraPalestrante(entidade.ID, _usuario.ID, entidade);
            return RedirectToAction("MinhasPalestras");
        }

        public ActionResult RemoverPalestra(string id)
        {
            Construtor.PalestraApp().RemoverPalestraDoPalestrante(id, _usuario.ID);
            return RedirectToAction("MinhasPalestras");
        }

        public ActionResult PalestrasRequisitadas()
        {
            return View(Construtor.EventoApp().EventosSolicidados(_usuario.ID));
        }

        public ActionResult PalestrasSubmetidas(string id)
        {
            _eventoId = id;
            return View(Construtor.PalestraApp().PalestrasSubmetidas(_usuario.ID, id));
        }

        public ActionResult ConfirmarPalestra(string eventoId, string palestraId, string coordenadorId, string resposta, string trilhaId)
        {
            Construtor.EventoApp().ConfirmarParticipacao(palestraId, eventoId, resposta, coordenadorId, trilhaId, _usuario.ID);
            return RedirectToAction("PalestrasSubmetidas/" + _eventoId);
        }

    }
}