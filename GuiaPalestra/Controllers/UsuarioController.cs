using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Areas.Cadastro.Controllers;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;
using Microsoft.AspNet.Identity;

namespace GuiaPalestrasOnline.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : Controller
    {
        private readonly Usuario _usuario;
        public UsuarioController()
        {
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MinhasPalestras()
        {
            return View(PalestraRepositorio.GetAll(_usuario.ID));
        }
        public ActionResult Inscrever(string id,bool confirmacao)
        {
            ChecaHorario(id);
            return RedirectToAction("MinhasPalestras");
        }

        public bool ChecaHorario(string palestraId)
        {
            var palestraTime = PalestraRepositorio.GetAll(_usuario.ID).FirstOrDefault(x => x.ID == palestraId);
            if (palestraTime != null)
            {


                foreach (var palestras in PalestraRepositorio.GetAll(_usuario.ID))
                {
                    if (palestras.ID != palestraTime.ID)
                    {
                        if (palestraTime.Duracao.Date == palestras.Duracao.Date)
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }
    }
}