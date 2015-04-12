using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;
using GuiaPalestrasOnline.ViewModel;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class EventoApp
    { 
        private readonly ICrud<Evento> repositorio;
        private readonly Contexto contexto;
        public EventoApp(ICrud<Evento> repo )
        {
            contexto=new Contexto();
            repositorio = repo;
        }
        public void Save(Evento entidade)
        {
            repositorio.Save(entidade);
        }
        public List<Evento> MeusEventos(string coordenadorId)
        {
           var listaDeEventosParaCoordeandor = contexto.SqlBd.Query<Evento>(
                "select e.Id as Id,e.Local,e.Tema,e.DiaInicial,e.DiaFinal from Evento e where @CoordenadorId = e.CoordenadorId",
                new
                {
                    CoordenadorId=coordenadorId
                }).ToList();
            return listaDeEventosParaCoordeandor;
        }
        public IEnumerable<PalestraSolicitadaViewModel> ListarPalestraDesseEvento(string TrilhaId,string CoordenadorId)
        {
          return  contexto.SqlBd.Query<PalestraSolicitadaViewModel>("select p.Titulo,p.Id PalestraId,pp.NomePalestrante Nome,pp.Id PalestranteId,pp.EmailPalestrante Email,ps.Pendencia,ps.EventoId  EventoId " +
                                 "from Palestra p,Palestrante pp, PalestraSolicitada ps,trilha t,Coordenador c " +
                                 "where p.Id=ps.PalestraId and " +
                                 "pp.Id=ps.PalestranteId and " +
                                 "t.Id=ps.TrilhaId and " +
                                 "c.Id=ps.CoordenadorId and "+
                                 "ps.TrilhaId=@trilhaId and ps.CoordenadorId =@coordenadorId and ps.Pendencia != 'recusada' ",new{trilhaId=TrilhaId,coordenadorId=CoordenadorId}).ToList();
        }
        public IEnumerable<Evento> EventosSolicidados(string palestranteId)
        {
          return  contexto.SqlBd.Query<Evento>(
                "select Id,Tema,Local from evento e,palestrasolicitada ps where ps.EventoId=e.Id and ps.PalestranteId= @pId group by(Tema)",
                new {pId = palestranteId}).ToList();
        }
        public void ResponderRequisicao(string palestraId, string eventoId,string resposta,string coordenadorId,string trilhaId)
        {
            if (resposta=="pendente" || resposta=="recusada" || resposta=="aceita" || resposta=="confirmada")
            {
                contexto.SqlBd.Query(
                "update palestrasolicitada set Pendencia = @R where PalestraId=@pId and EventoId = @eId", new
                {
                    R = resposta,
                    pId = palestraId,
                    eId = eventoId
                });
            }
            
            if (resposta=="confirmada")
            {
                contexto.SqlBd.Query(
                    "insert into palestraevento (EventoId,PalestraId,CoordenadorId,TrilhaId) values(@eId,@pId,@cId,@tId)",
                    new {eId = eventoId, pId = palestraId, cId = coordenadorId,tId=trilhaId});
            }
        }
        public IEnumerable<Evento> GetAll()
        {
           return repositorio.GetAll();
        }

        public IEnumerable<PalestraSolicitadaViewModel> PalestrasRegistradadas(string eventoId,string coordenadorId)
        {
          return contexto.SqlBd.Query<PalestraSolicitadaViewModel>("select p.Titulo,p.Id palestraId,pe.EventoId EventoId,t.NomeTrilha,t.Id trilhaId,p.PalestranteId ,pp.NomePalestrante Nome,pp.EmailPalestrante Email from " +
                                                                   "Palestra p,Trilha t,PalestraEvento pe,Palestrante pp " +
                                                                   "where p.Id = pe.PalestraId " +
                                                                   "and t.Id = pe.TrilhaId " +
                                                                   "and p.PalestranteId = pp.Id " +
                                                                   "and pe.EventoId = @eId and pe.CoordenadorId = @cId",
                                                                   new{eId=eventoId,cId=coordenadorId}).ToList();
        } 
    }
}