using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;
using Microsoft.SqlServer.Server;

namespace GuiaPalestra.Api
{
    
    public class TrilhaApiController : ApiController
    {
        public string Get()
        {
            return "Lista de trilhas";
        }

        public HttpResponseMessage Post(Trilha trilha)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            Construtor.TrilhaApp().Save(trilha);
            return Request.CreateResponse(HttpStatusCode.Created, trilha);
        }
        public HttpResponseMessage Put(string id,Trilha trilha)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            Construtor.TrilhaApp().Update(trilha);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public HttpResponseMessage Delete(string id)
        {
            Construtor.TrilhaApp().Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
