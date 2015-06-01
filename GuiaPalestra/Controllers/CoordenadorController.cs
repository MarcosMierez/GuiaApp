using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestra.Controllers
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

            return View(new Evento());
        }
        [HttpPost]
        public ActionResult CriarEvento(Evento entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.CoordenadorId = Seguranca.Usuario().ID;
                Construtor.EventoApp().Save(entidade);
                EventoId = entidade.ID;
                return RedirectToAction("CriarTrilha");
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
            return View(Construtor.TrilhaApp().MinhasTrilhas(Seguranca.Usuario().ID, EventoId));
        }

        public ActionResult PalestrasParaEssaTrilha(string id)
        {
            TrilhaId = id;
            ViewBag.eventoID = EventoId;
            ViewBag.nomeTrilha = new Contexto().SqlBd.Query<string>("select NomeTrilha from Trilha where Id = @trilhaId",
                new { trilhaId = id }).FirstOrDefault();
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
            return RedirectToAction("ListarPalestrasDesseEvento/"+EventoId,"Coordenador");
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

        public ActionResult AceitarPalestra(string id, string eventoId, string resposta)
        {
            Construtor.EventoApp().ResponderRequisicao(id, eventoId, resposta, "", "","");
            return RedirectToAction("PalestrasParaEssaTrilha/" + TrilhaId);
        }

        public ActionResult GerenciarEvento(string id)
        {
            EventoId = id;
            ViewBag.eventId = id;
            return View(Construtor.EventoApp().PalestrasRegistradas(id, Seguranca.Usuario().ID));
        }

        public ActionResult EditarPalestraEvento(string id)
        {
            var palestra = Construtor.EventoApp().DetalhePalestra(id, Seguranca.Usuario().ID, EventoId);
            var salas = Construtor.SalaApp().MinhasSalas(EventoId);
            ViewBag.salas = new SelectList(salas, "ID", "NumeroSala",palestra.SalaId);
            ViewBag.datas = GetDatas();
            return View(palestra);
        }
        [HttpPost]
        public ActionResult EditarPalestraEvento(PalestraSolicitadaViewModel palestra)
        {
            if (!ModelState.IsValid) return RedirectToAction("GerenciarEvento/" + EventoId);
            palestra.CoordenadorId = Seguranca.Usuario().ID;
            Construtor.EventoApp().PreencherPalestraParaEvento(palestra);

            return RedirectToAction("GerenciarEvento/" + EventoId);
        }

        public ActionResult PublicarEvento(string id)
        {
            new Contexto().SqlBd.Query("update evento set Status = 'Usuario' where Id = @eId",new{eId=id});
            return RedirectToAction("GerenciarEvento/" + id, "Coordenador");
        }

        public IEnumerable<Datas> GetDatas()
        {
            if (EventoId != null)
            {
                var evento = Construtor.EventoApp().GetById(EventoId);
                var diaInicial = evento.DiaInicial;
                var diaaFinal = evento.DiaFinal;
                var ListaDatas = new List<Datas>();
                if (diaInicial.Month == diaaFinal.Month)
                {
                    for (int i = diaInicial.Day; i <= diaaFinal.Day; i++)
                    {
                        var tempData = new Datas
                        {
                            data= (Convert.ToDateTime(i + "/" + diaInicial.Month + "/" + diaInicial.Year))
                        };
                        ListaDatas.Add(tempData);
                    }
                }
                return ListaDatas;
            }
            return new List<Datas>();
        }

        public struct Datas
        {
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime data { get; set; }
        }

        public ActionResult GerarRelatorioEvento(string id)
        {
            return View(Construtor.EventoApp().UsuarioConfirmados(id));
        }

    }
}