using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;

namespace GuiaPalestra.Controllers
{
    [Authorize(Roles ="Palestrante")]
    public class EventoController : Controller
    {
        private static string _eventoId;
        private static string _coordenadorId;
        private static string _trilhaId;
        private Usuario usuarioLogado;

        public EventoController()
        {
            usuarioLogado = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View(Construtor.EventoApp().GetAll());
        }

        public ActionResult TrilhasDeEvento(string eventoId,string coordenadorId)
        {
            _eventoId = eventoId;
            _coordenadorId = coordenadorId;
            var trilhas = Construtor.TrilhaApp().MinhasTrilhas(coordenadorId,eventoId);
            return View(trilhas);
        }

        public ActionResult SubmeterPalestras(string id)
        {
            _trilhaId = id;
            var todasPalestrasPalestrante = Construtor.PalestraApp().TodasPalestrantePalestras(usuarioLogado.ID);
            var palestrasSubmetidas = Construtor.PalestraApp().PalestrasSubmetidas(usuarioLogado.ID, _eventoId);
            var palestrasVm = new List<Palestra>();
            checaPalestras(todasPalestrasPalestrante, palestrasSubmetidas, palestrasVm);
            return View(palestrasVm);
        }

        private static void checaPalestras(IEnumerable<Palestra> todasPalestrasPalestrante, IEnumerable<PalestraSolicitadaViewModel> palestrasSubmetidas,
            ICollection<Palestra> palestrasVm)
        {
            foreach (var palestra in todasPalestrasPalestrante)
            {
                var palestraSubmetida = palestrasSubmetidas.FirstOrDefault(x => x.PalestraId == palestra.ID);
                if (palestraSubmetida == null)
                {
                    palestrasVm.Add(palestra);
                }
            }
        }

        public ActionResult DisponibilizarPalestra(string id)
        {
            Construtor.PalestraApp().DisponibilizarPalestra(id,usuarioLogado.ID,_eventoId,_trilhaId,"pendente",_coordenadorId);
            return RedirectToAction("SubmeterPalestras/"+_trilhaId);
        }
    }
}