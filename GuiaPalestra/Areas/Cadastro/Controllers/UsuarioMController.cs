using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Areas.Cadastro.Controllers
{
    public class UsuarioMController : Controller
    {
        // GET: Cadastro/UsuarioM
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Usuario entidade)
        {
            Construtor.UsuarioApp().Save(entidade);
            return View();
        }

        public ActionResult Update(string id)
        {
            return View(Construtor.UsuarioApp().GetById(id));

        }
        [HttpPost]
        public ActionResult Update(Usuario entidade)
        {
            Construtor.UsuarioApp().Update(entidade);
            return RedirectToAction("TodosUsuarios");

        }

        public ActionResult TodosUsuarios()
        {
            var usuarios = Construtor.UsuarioApp().GetAll();
            return View(usuarios);
        }

    }
}