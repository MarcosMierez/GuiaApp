using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Controllers
{
    [Authorize(Roles = "Palestrante")]
    public class PalestrantePainelController : Controller
    {
        private readonly Usuario _usuario;
        public static string _eventoId;

        public PalestrantePainelController()
        {
            _usuario = Seguranca.Usuario();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CriarPalestras()
        {
            var listaHorarios = new List<Horarios>()
            {
                new Horarios
                {
                    horarioInicial = Convert.ToDateTime("00:30:00")
                },
                new Horarios
                {
                    horarioInicial = Convert.ToDateTime("01:00:00")
                },
                new Horarios
                {
                    horarioInicial = Convert.ToDateTime("01:30:00")
                }
            };


            ViewBag.horarios = new SelectList(listaHorarios, "horarioInicial", "horarioInicial");
            return View(new Palestra());
        }
        [HttpPost]
        public ActionResult CriarPalestras(Palestra entidade)
        {
            if (ModelState.IsValid)
            {
                entidade.PalestranteId = _usuario.ID;
                Construtor.PalestraApp().Save(entidade);
                this.Flash("Palestra registrada com sucesso");
                return RedirectToAction("Index");
            }
            this.Flash("Preencha todos os campos", LoggerEnum.Error);
            return View(entidade);
        }

        public ActionResult MinhasPalestras()
        {

            return View(Construtor.PalestraApp().TodasPalestrantePalestras(_usuario.ID));
        }

        public ActionResult EditarPalestra(string id)
        {
            return View(Construtor.PalestraApp().ListarPalestrasDoPalestrantePorId(id, _usuario.ID));
        }
        [HttpPost]
        public ActionResult EditarPalestra(Palestra entidade)
        {
            Construtor.PalestraApp().AlterarPalestraPalestrante(entidade.ID, _usuario.ID, entidade);
            return RedirectToAction("MinhasPalestras");
        }

        public ActionResult RemoverPalestra(string id, string eventoId)
        {
            Construtor.PalestraApp().RemoverPalestraDoPalestrante(id, _usuario.ID, eventoId);
            return RedirectToAction("PalestrasSubmetidas/" + eventoId, "PalestrantePainel");
        }

        public ActionResult DeletarPalestra(string id)
        {
            Construtor.PalestraApp().DeletarPalestraDoPalestrante(id, _usuario.ID);
            return RedirectToAction("MinhasPalestras", "PalestrantePainel");
        }
        public ActionResult PalestrasRequisitadas()
        {
            return View(Construtor.EventoApp().EventosSolicidados(_usuario.ID));
        }

        public ActionResult PalestrasSubmetidas(string id)
        {
            _eventoId = id;
            return View(Construtor.PalestraApp().PalestrasSubmetidas(_usuario.ID, id));
        }

        public ActionResult ConfirmarPalestra(string eventoId, string palestraId, string coordenadorId, string resposta, string trilhaId)
        {
          var retorno =  Construtor.EventoApp().ConfirmarParticipacao(palestraId, eventoId, resposta, coordenadorId, trilhaId, _usuario.ID);
            if (retorno==false)
            {
               this.Flash("Voce ja esta registrado em outro evento nesse mesmo periodo de dias",LoggerEnum.Error); 
            }
            else
            {
                this.Flash("Confirmacao concluida"); 
                
            }
            return RedirectToAction("PalestrasSubmetidas/" + _eventoId);
        }
        public class Horarios
        {
            public DateTime horarioInicial { get; set; }
        }
    }
}