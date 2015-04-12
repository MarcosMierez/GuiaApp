using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Areas.Cadastro.Controllers
{
    public class TrilhaController : Controller
    {
        // GET: Cadastro/Trilha
        public ActionResult Index()
        {
            return View();
        }    
        public ActionResult Cadastrar()
        {
            return View(new Trilha());
        }
        [HttpPost]
        public ActionResult Cadastrar(Trilha entidade)
        {
            Construtor.TrilhaApp().Save(entidade);
            return RedirectToAction("Index");
        }

        public ActionResult Detalhe(string Id)
        {
            return View(Construtor.TrilhaApp().GetById(Id));
        }

        public ActionResult Editar(string Id)
        {
            return View(Construtor.TrilhaApp().GetById(Id));
        }
        [HttpPost]
        public ActionResult Editar(Trilha entidade)
        {
            Construtor.TrilhaApp().Update(entidade);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Construtor.TrilhaApp().Delete(id);
            return RedirectToAction("Index");
        }
    }
}