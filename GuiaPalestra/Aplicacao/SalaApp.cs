using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Models;

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
    }
}