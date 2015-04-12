using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Areas.Cadastro.Controllers
{
    public class CoordenadorMController : Controller
    {
        // GET: Cadastro/CoordenadorM
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
           Construtor.CoordenadorApp().Save(entidade);
            return  RedirectToAction("Index");
        }

        public ActionResult Editar(string id)
        {
            return View(Construtor.CoordenadorApp().GetById(id));
        }
        [HttpPost]
        public ActionResult Editar(Coordenador entidade)
        {
            Construtor.CoordenadorApp().Update(entidade);
            return RedirectToAction("Index");
        }
    }
}