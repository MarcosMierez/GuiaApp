using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;
using GuiaPalestrasOnline.ViewModel;

namespace GuiaPalestrasOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(Construtor.EventoApp().GetAll());
        }

        public ActionResult PalestrasParaEsteEvento(string id)
        {
            var _usuario = Seguranca.Usuario();

            return RedirectToAction("Index/"+id,"Cadastro/Palestra");
        }
        
    }
}