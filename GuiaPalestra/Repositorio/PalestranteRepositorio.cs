using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Repositorio
{
    public class PalestranteRepositorio:ICrud<Palestrante>

    {
        private readonly Contexto context;

        public PalestranteRepositorio()
        {
            context = new Contexto();
        }
        public void Save(Palestrante entidade)
        {
            context.SqlBd.Query(
                "insert into palestrante (Id,NomePalestrante,TwitterPalestrante) values(@Id,@Nome,@twitter)",
                new {Id = entidade.ID, Nome = entidade.Nome, twitter = entidade.TwitterPalestrante});
        }

        public void Update(Palestrante entidade)
        {
            context.SqlBd.Query("update palestrante set NomePalestrante = @nome,TwitterPalestrante = @twitter where Id =@id",
                new {nome = entidade.Nome, twitter = entidade.TwitterPalestrante,id=entidade.ID});
        }

        public void Delete(string Id)
        {
            context.SqlBd.Query("delete from palestrante where Id = @id", new {id = Id});
        }

        public Palestrante GetByID(string Id)
        {
          var temp=context.SqlBd.Query<Palestrante>("select Id,NomePalestrante as Nome,TwitterPalestrante from palestrante where Id = @ID", new {ID = Id}).FirstOrDefault();
            return temp;
        }

        public IEnumerable<Palestrante> GetAll()
        {
            return context.SqlBd.Query<Palestrante>("select Id,NomePalestrante as Nome,TwitterPalestrante from palestrante").ToList();

        }
    }
}