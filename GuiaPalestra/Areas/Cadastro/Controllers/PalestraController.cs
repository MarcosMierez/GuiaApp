using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Areas.Cadastro.Controllers
{
    [Authorize]
    public class PalestraController : Controller
    {
        private readonly Contexto repositorio;
        private readonly Usuario _usuario;
        public static string eventoId;
        public PalestraController()
        {
            repositorio = new Contexto();
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index(string id)
        {
            eventoId = id;
        //    var todasPalestras = Construtor.EventoApp().ListarPalestraDesseEvento(id);           
        //    var minhasPalestras = PalestraRepositorio.GetAll(_usuario.ID);
        //    var palestrasDisponiveisParaUsuario = (from palestra in todasPalestras let minhaPalestra = minhasPalestras.FirstOrDefault(x => x.ID == palestra.ID) where minhaPalestra == null select palestra).ToList();
       //     return View(palestrasDisponiveisParaUsuario);
            return RedirectToAction("Index");

        }
        public ActionResult Detalhe(string Id)
        {
            return View(Construtor.PalestraApp().GetById(Id));
        }
        public ActionResult Editar(string Id)
        {
            GetValue(Construtor.PalestraApp().GetById(Id));
            return View(Construtor.PalestraApp().GetById(Id));
        }
        [HttpPost]
        public ActionResult Editar(Palestra entidade)
        {
            Construtor.PalestraApp().Update(entidade);
            return RedirectToAction("Index");
        }
        private void GetValue(Palestra palestra)
        {
            var palestrantes = Construtor.PalestranteApp().Details(palestra.Palestrante.ID);           
            ViewBag.palestrantes = palestrantes.Nome;
        }

        public ActionResult Inscrever(string ID)
        {
            Construtor.UsuarioApp().ParticiparPalestra(ID,_usuario.ID,eventoId,"solicitada");
            return RedirectToAction("Index/"+eventoId);
        }
    }
}