using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class TrilhaApp
    {
        private readonly ICrud<Trilha> repositorio;
        private readonly Contexto contexto;
        public TrilhaApp(ICrud<Trilha>repo )
        {
            repositorio = repo;
            contexto=new Contexto();
        }

        public IEnumerable<Trilha> MinhasTrilhas(string coordenadorId,string eventoId)
        {
          return  contexto.SqlBd.Query<Trilha>("select Id,NomeTrilha,CoordenadorId from trilha where coordenadorId = @cId and EventoId = @eId", new {cId = coordenadorId,eId=eventoId}).ToList();
        } 
        public Trilha GetById(string id) 
        {
           return repositorio.GetByID(id);
        }
        public void Save(Trilha entidade)
        {
            repositorio.Save(entidade);
        }
        public void Update(Trilha entidade)
        {
            repositorio.Update(entidade);
        }

        public void Delete(string Id)
        {
            repositorio.Delete(Id);
        }
    }
}