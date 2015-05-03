using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestra.Models;
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
                "insert into palestrante (Id,NomePalestrante,TwitterPalestrante,EmailPalestrante,Permissao,Senha,Foto) values(@Id,@Nome,@twitter,@email,@claim,@password,'GenericPhoto.png')",
                new {Id = entidade.ID, Nome = entidade.Nome.ToLower(), twitter = entidade.TwitterPalestrante.ToLower(),email=entidade.Email.ToLower(),claim="Palestrante",password=entidade.Senha.ToLower()});
        }

        public void Update(Palestrante entidade)
        {
            context.SqlBd.Query("update palestrante set NomePalestrante = @nome,TwitterPalestrante = @twitter,Foto = @photo where Id =@id",
                new {nome = entidade.Nome, twitter = entidade.TwitterPalestrante,photo=entidade.Foto,id=entidade.ID});
        }

        public void Delete(string Id)
        {
            context.SqlBd.Query("delete from palestrante where Id = @id", new {id = Id});
        }

        public Palestrante GetByID(string Id)
        {
        return context.SqlBd.Query<Palestrante>("select Id,NomePalestrante as Nome,TwitterPalestrante,Foto from palestrante where Id = @ID", new {ID = Id}).FirstOrDefault();
            
        }

        public IEnumerable<Palestrante> GetAll()
        {
            return context.SqlBd.Query<Palestrante>("select Id,NomePalestrante as Nome,TwitterPalestrante from palestrante").ToList();

        }
    }
}