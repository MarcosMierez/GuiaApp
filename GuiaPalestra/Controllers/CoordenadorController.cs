using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;
using GuiaPalestrasOnline.ViewModel;

namespace GuiaPalestrasOnline.Controllers
{
    [Authorize(Roles = "coordenador")]
    public class CoordenadorController : Controller
    {
        public static string EventoId;
        public static string TrilhaId;
        public ActionResult Index()
        {
            return View(Seguranca.Usuario());
        }
        public ActionResult CriarEvento()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult CriarEvento(Evento entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.CoordenadorId = Seguranca.Usuario().ID;
                Construtor.EventoApp().Save(entidade);
                return RedirectToAction("Index");
            }
            
            return View(entidade);
        }
        public ActionResult MeusEventos()
        {
            return View(Construtor.EventoApp().MeusEventos(Seguranca.Usuario().ID));
        }
        public ActionResult ListarPalestrasDesseEvento(string ID)
        {
            EventoId = ID;
            return View(Construtor.TrilhaApp().MinhasTrilhas(Seguranca.Usuario().ID,ID));
        }

        public ActionResult PalestrasParaEssaTrilha(string id)
        {
            TrilhaId = id;
            ViewBag.nomeTrilha = new Contexto().SqlBd.Query<string>("select NomeTrilha from Trilha where Id = @trilhaId",
                new {trilhaId = id}).FirstOrDefault();
            var list = Construtor.EventoApp().ListarPalestraDesseEvento(id, Seguranca.Usuario().ID);
            return View(list);
        }

        public ActionResult PalestrasRequisitadasParaEssaTrilha(string id)
        {
            return View();
        }

        public ActionResult CriarTrilha()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult CriarTrilha(Trilha entidade)
        {
            entidade.CoordenadorId = Seguranca.Usuario().ID;
            entidade.EventoId = EventoId;
            Construtor.TrilhaApp().Save(entidade);
            return RedirectToAction("CriarTrilha");
        }

        public ActionResult CriarSala()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CriarSala(Sala entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.EventoId = EventoId;
               Construtor.SalaApp().Save(entidade);
                return RedirectToAction("Index");
            }
            return View(entidade);
        }

        public ActionResult AceitarPalestra(string id, string eventoId,string resposta)
        {
            Construtor.EventoApp().ResponderRequisicao(id,eventoId,resposta,"","");
            return RedirectToAction("PalestrasParaEssaTrilha/"+TrilhaId);
        }

        public ActionResult GerenciarEvento(string id)
        {
            return View(Construtor.EventoApp().PalestrasRegistradadas(id, Seguranca.Usuario().ID));
        }
        private static List<Palestra> RetornaPalestrasDisponiveisParaEvento(IEnumerable<Palestra> todasPalestras, IEnumerable<PalestraSolicitadaViewModel> minhasPalestras)
        {
            return (from palestra in todasPalestras let tempPalestra = minhasPalestras.FirstOrDefault(x => x.PalestraId == palestra.ID) where tempPalestra == null select palestra).ToList();
        }//nao queira entender esse foreach
    }
}