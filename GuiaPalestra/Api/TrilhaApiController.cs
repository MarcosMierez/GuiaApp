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
        public IEnumerable<Trilha> Get()
        {
            var trilhas = new List<Trilha>();
            var trilha1 = new Trilha(){CoordenadorId = "1",EventoId = "1",ID = "1",NomeTrilha = "OO"};
            var trilha2 = new Trilha() { CoordenadorId = "2", EventoId = "2", ID = "2", NomeTrilha = "PE" };
            var trilha3 = new Trilha() { CoordenadorId = "3", EventoId = "3", ID = "3", NomeTrilha = "BD" };
            trilhas.Add(trilha1);
            trilhas.Add(trilha2);
            trilhas.Add(trilha3);

            return trilhas;

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
