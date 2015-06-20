using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;

namespace GuiaPalestra.Api
{
    public class UsuarioApiController : ApiController
    {
        public IEnumerable<Evento> Get()
        {
            return Construtor.EventoApp().EventosDisponiveis("Usuario");
        } 
    }
}
