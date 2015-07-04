using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;

namespace GuiaPalestra.Api
{
    public class UsuarioApiController : ApiController
    {
        public IEnumerable<Evento> Get()
        {
            return Construtor.EventoApp().EventosDisponiveis("Usuario");
        }

        public IEnumerable<PalestraViewModel> PalestrasDesseEvento(string id,string usuarioId)
        {

                 return Construtor.EventoApp().PalestrasDisponiveisEvento(id, usuarioId);         
        }

        public HttpResponseMessage InscreverPalestra(string id,string eventoId,string usuarioId,bool status,string trilhaId,string salaId)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            Construtor.EventoApp().InscreverPalestraEvento(id, eventoId, usuarioId, status, trilhaId, salaId);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        public IEnumerable<Evento> EventosUsuario(string usuarioId)
        {
            return Construtor.EventoApp().EventosUsuario(usuarioId);
        }

        public IEnumerable<PalestraViewModel> MinhasPalestrasEvento(string eventoId,string usuarioId)
        {
            return Construtor.EventoApp().PalestrasAdicionadas(eventoId,usuarioId);
        } 


    }
}
