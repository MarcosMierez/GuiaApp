using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;

namespace GuiaPalestra.Controllers
{
    public class HomeController : Controller
    {
      [AllowAnonymous]
        public ActionResult Index()
        {
            return View(Construtor.EventoApp().EventosDisponiveis("Usuario"));
        }

    }
}