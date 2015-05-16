using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class PalestraApp
    {
        private readonly ICrud<Palestra> repositorio;
        private readonly Contexto contexto;
        public PalestraApp(ICrud<Palestra> repository)
        {
            repositorio = repository;
            contexto = new Contexto();

        }
        public IEnumerable<Palestra> GetAll()
        {
            return repositorio.GetAll();
        }
        public void Save(Palestra entidade)
        {
            repositorio.Save(entidade);
        }
        public Palestra GetById(string id)
        {
            return repositorio.GetByID(id);
        }
        public void Update(Palestra entidade)
        {
            repositorio.Update(entidade);
        }
        public IEnumerable<Palestra> TodasPalestrantePalestras(string idPalestrante)
        {
            var palestra = contexto.SqlBd.Query<Palestra>("select ID,Titulo,Duracao,Status from palestra where palestranteId = @Id", new { Id = idPalestrante }).ToList();
            return palestra;
        }
        public Palestra ListarPalestrasDoPalestrantePorId(string idPalestra, string idPalestrante)
        {
            return contexto.SqlBd.Query<Palestra>("select ID,Titulo,Duracao from palestra where ID = @Id and palestranteID = @pId", new { Id = idPalestra, pId = idPalestrante }).FirstOrDefault();
        }
        public void AlterarPalestraPalestrante(string idPalestra, string idPalestrante, Palestra entidade)
        {
            contexto.SqlBd.Query(
                "update palestra set Titulo = @titulo , Duracao = @duracao where ID = @pId and palestranteId = @ppID",
                new
                {
                    titulo = entidade.Titulo,
                    duracao = entidade.Duracao,
                    pId = idPalestra,
                    ppId = idPalestrante
                });
        }
        public void RemoverPalestraDoPalestrante(string idPalestra, string idPalestrante,string eventoId)
        {
            contexto.SqlBd.Query("delete from palestrasolicitada where PalestraId = @pId and PalestranteId = @ppId and EventoId = @eid ",
                new { pId = idPalestra, ppId = idPalestrante,eid=eventoId });
        }
        public void DeletarPalestraDoPalestrante(string idPalestra, string idPalestrante)
        {
            contexto.SqlBd.Query("delete from palestra where Id = @pId and PalestranteId = @ppId ",
                new { pId = idPalestra, ppId = idPalestrante});
            contexto.SqlBd.Query("delete from palestrasolicitada where PalestraId = @pId and PalestranteId = @ppId ",
                new { pId = idPalestra, ppId = idPalestrante });
        }


        public void DisponibilizarPalestra(string palestraId,string palestranteId,string eventoId,string trilhaId,string pendencia,string coordenadorId)
        {
            if (pendencia=="pendente" || pendencia=="confirmada" || pendencia=="aceita")
            {
                contexto.SqlBd.Query("insert into palestrasolicitada (PalestraId,PalestranteId,EventoId,TrilhaId,Pendencia,CoordenadorId) values (@pId,@ppId,@eId,@tId,@p,@cId)",
                new{pId=palestraId,ppId=palestranteId,eId=eventoId,tId=trilhaId,p=pendencia,cId=coordenadorId});
            }
            
        }

        public IEnumerable<PalestraSolicitadaViewModel> PalestrasSubmetidas(string palestranteId,string eventoId)
        {
          return  contexto.SqlBd.Query<PalestraSolicitadaViewModel>("select p.Titulo,p.Id PalestraId,pp.NomePalestrante Nome,pp.Id PalestranteId,pp.EmailPalestrante Email,ps.Pendencia,t.Id as TrilhaId,t.NomeTrilha NomeTrilha,ps.EventoId EventoId,c.Id CoordenadorId " +
                                 "from Palestra p,Palestrante pp, PalestraSolicitada ps,trilha t,Coordenador c " +
                                 "where p.Id=ps.PalestraId and " +
                                 "pp.Id=ps.PalestranteId and " +
                                 "t.Id=ps.TrilhaId and " +
                                 "c.Id=ps.CoordenadorId and " +
                                 "ps.PalestranteId=@PiD and ps.EventoId =@EiD",new{PiD=palestranteId,EiD=eventoId}).ToList();
        } 
        public void SolicitarPalestraParaEvento(string palestraId, string eventoId, string palestranteId, string coordenadorId)
        {
            contexto.SqlBd.Query("insert into palestrasolicitada (palestraId,eventoId,palestranteId,coordenadorId,Pendencia) values(@PiD,@eiD,@PPiD,@CiD,'pendente')", new { Pid = palestraId, eiD = eventoId, PPid = palestranteId, CiD = coordenadorId });
        }

        public void DisponibilizarPalestraParaOEvento(string eventoId, string palestraId, string coordenadorId, string resposta,string trilhaId)
        {
            switch (resposta)
            {
                case "sim":
                    if (!string.IsNullOrEmpty(eventoId) && !string.IsNullOrEmpty(palestraId) && !string.IsNullOrEmpty(coordenadorId))
                    {
                        contexto.SqlBd.Query("insert into palestraevento (EventoId,PalestraId,CoordenadorId) values(@eId,@pId,@cId)", new
                        {
                            eId = eventoId,
                            pId = palestraId,
                            cId = coordenadorId
                        });
                        contexto.SqlBd.Query("update palestrasolicitada set Pendencia = 'aceita' where EventoId = @eId and PalestraId = @pId",
                            new { eId = eventoId, pId = palestraId });
                    }
                    break;
                default:
                    contexto.SqlBd.Query("update palestrasolicitada set Pendencia = 'recusada' where EventoId = @eId and PalestraId = @pId",
                        new { eId = eventoId, pId = palestraId });
                    break;
            }
        }

        public IEnumerable<PalestraSolicitadaViewModel> PalestrasSolicitadadasParaEvento(string eventoId)
        {
            return
                 contexto.SqlBd.Query<PalestraSolicitadaViewModel>(
                     "SELECT pl.Id palestraId,pl.Titulo,e.Id eventoId,e.Local FROM palestrasolicitada p,palestrante pp,palestra pl,evento e where " +
                     "p.PalestranteId=pp.Id " +
                     "and p.EventoId=e.Id " +
                     "and p.PalestraId=pl.Id " +
                     "and e.Id= @EventoId",
                     new { EventoId = eventoId }).ToList();
        }
    }
}