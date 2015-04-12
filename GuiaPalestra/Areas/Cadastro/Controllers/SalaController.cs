using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Areas.Cadastro.Controllers
{
    public class SalaController : Controller
    {
        // GET: Cadastro/Sala
        public ActionResult Index()
        {
            return View(Construtor.SalaApp().GetAll());
        }

        public ActionResult Cadastrar()
        {
            return View(new Sala());
        }
        [HttpPost]
        public ActionResult Cadastrar(Sala entidade)
        {
            Construtor.SalaApp().Save(entidade);
            return RedirectToAction("Index");
        }

        public ActionResult Detalhe(string id)
        {
            return View(Construtor.SalaApp().GetById(id));
        }
        public ActionResult Editar(string id)
        {
            return View(Construtor.SalaApp().GetById(id));
        }
        [HttpPost]
        public ActionResult Editar(Sala entidade)
        {
            Construtor.SalaApp().Update(entidade);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Construtor.SalaApp().Delete(id);
            return RedirectToAction("Index");
        }
    }
}