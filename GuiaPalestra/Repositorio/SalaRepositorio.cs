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
    public class SalaRepositorio:ICrud<Sala>
    {
        private readonly Contexto contexto;
        public SalaRepositorio()
        {
            contexto=new Contexto();
        }
        public void Save(Sala entidade)
        {
            contexto.SqlBd.Query("insert into sala (Id,NumeroSala,Descricao,EventoId,Vagas) values(@Id,@numero,@descricao,@eventoId,@vagas)",
                new {Id = entidade.ID, numero = entidade.NumeroSala, descricao = entidade.Descricao,eventoId=entidade.EventoId,vagas=entidade.Vagas});
        }

        public void Update(Sala entidade)
        {
            contexto.SqlBd.Query("update sala set NumeroSala =@numero, Descricao = @descricao where Id = @id",
                new {numero = entidade.NumeroSala, descricao = entidade.Descricao,id=entidade.ID});
        }

        public void Delete(string Id)
        {
            contexto.SqlBd.Query("delete from sala where Id = @id",
                new {id = Id});
        }

        public Sala GetByID(string Id)
        {
           return contexto.SqlBd.Query<Sala>("select Id,NumeroSala,Descricao from sala where Id = @id",
                new {id = Id}).First();
        }

        public IEnumerable<Sala> GetAll()
        {
           return contexto.SqlBd.Query<Sala>("select * from sala").ToList();
        }
    }
}