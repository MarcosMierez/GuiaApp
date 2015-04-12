using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Areas.Cadastro.Controllers
{ 
    public class PalestranteController : Controller
    {
        // GET: Cadastro/Palestrante
       
        public ActionResult Index()
        {
            
            return View(Construtor.PalestranteApp().GetAll());
        }

        public ActionResult Cadastrar()
        {
            return View(new Palestrante());
        }
        [HttpPost]
        public ActionResult Cadastrar(Palestrante entidade)
        {
            Construtor.PalestranteApp().Save(entidade);
            return RedirectToAction("Index");
        }
        public ActionResult Detalhe(string Id)
        {
          return  View(Construtor.PalestranteApp().Details(Id));
        }

        public ActionResult Editar(string id)
        {
            return View(Construtor.PalestranteApp().Details(id));
        }
        [HttpPost]
        public ActionResult Editar(Palestrante entidade)
        {
           Construtor.PalestranteApp().Edit(entidade);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Construtor.PalestranteApp().Remove(id);
            return RedirectToAction("Index");
        }

    }
}