using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class SalaApp
    {
        private readonly ICrud<Sala> repositorio;

        public SalaApp(ICrud<Sala>repository )
        {
            repositorio = repository;
        }

        public void Save(Sala entidade)
        {
            repositorio.Save(entidade);
        }

        public Sala GetById(string Id)
        {
           return repositorio.GetByID(Id);
        }

        public IEnumerable<Sala> GetAll()
        {
            return repositorio.GetAll();
        }

        public void Update(Sala entidade)
        {
            repositorio.Update(entidade);
        }

        public void Delete(string Id)
        {
            repositorio.Delete(Id);
        }

        public IEnumerable<Sala> MinhasSalas(string eventoId)
        {
            return new Contexto().SqlBd.Query<Sala>("select Id,NumeroSala from sala where EventoId = @eId",new{eId=eventoId}).ToList();
        } 
    }
}