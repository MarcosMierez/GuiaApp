using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;

namespace GuiaPalestra.Api
{
   
    public class UsuarioApiController : ApiController
    {
        [Route("api/eventos")]
        [HttpGet]
        public IEnumerable<Evento> GetEventos()
        {
            return Construtor.EventoApp().EventosDisponiveis("Usuario");
        }

        [Route("api/eventos/{id}/{usuarioId}")]
        [HttpGet]
        public IEnumerable<PalestraViewModel> Get(string id, string usuarioId)
        {

            return Construtor.EventoApp().PalestrasDisponiveisEvento(id, usuarioId);
        }


        [Route("api/eventos")]
        [HttpPost]
        public HttpResponseMessage Post(string id, string eventoId, string usuarioId, bool status, string trilhaId, string salaId)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            Construtor.EventoApp().InscreverPalestraEvento(id, eventoId, usuarioId, status, trilhaId, salaId);
            return Request.CreateResponse(HttpStatusCode.Created);
        }


        [Route("api/eventos/{usuarioId}")]
        [HttpGet]
        public IEnumerable<Evento> Get(string usuarioId)
        {
            return Construtor.EventoApp().EventosUsuario(usuarioId);
        }


        [Route("api/eventos/palestras/{eventoId}/{usuarioId}")]
        public IEnumerable<PalestraViewModel> GetMinhasPalestrasEvento(string eventoId, string usuarioId)
        {
            return Construtor.EventoApp().PalestrasAdicionadas(eventoId, usuarioId);
        }


    }
}
