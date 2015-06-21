using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Repositorio
{
    public class TrilhaRepositorio:ICrud<Trilha>
    {
        private readonly Contexto contexto;

        public TrilhaRepositorio()
        {
            contexto=new Contexto();
        }
        public void Save(Trilha entidade)
        {
            contexto.SqlBd.Query("insert into trilha(Id,NomeTrilha,CoordenadorId,EventoId) values(@id,@nome,@CiD,@eId)",
                new {id = entidade.ID, nome = entidade.NomeTrilha,CiD=entidade.CoordenadorId,eiD=entidade.EventoId});
        }

        public void Update(Trilha entidade)
        {
            contexto.SqlBd.Query("update trilha set NomeTrilha = @nome , EventoId = @eventoId where Id = @Id", new {nome=entidade.NomeTrilha, Id = entidade.ID,eventoId=entidade.EventoId});
        }

        public void Delete(string Id)
        {
            contexto.SqlBd.Query("delete from trilha where Id = @Id",new{Id=Id});
        }

        public Trilha GetByID(string Id)
        {
           return contexto.SqlBd.Query<Trilha>("select Id,NomeTrilha from trilha where Id = @ID",new{ID=Id}).First();
        }

        public IEnumerable<Trilha> GetAll()
        {
            return contexto.SqlBd.Query<Trilha>("select * from trilha").ToList();
        }
    }
}