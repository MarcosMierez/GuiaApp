using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestra.ViewModel;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestra.Aplicacao
{
    public class EventoApp
    {
        private readonly ICrud<Evento> repositorio;
        private readonly Contexto contexto;
        public EventoApp(ICrud<Evento> repo)
        {
            contexto = new Contexto();
            repositorio = repo;
        }
        public void Save(Evento entidade)
        {
            repositorio.Save(entidade);
        }
        public IEnumerable<Evento> GetAll()
        {
            return repositorio.GetAll();
        }

        public Evento GetById(string id)
        {
            return repositorio.GetByID(id);
        }
        public IEnumerable<Evento> EventosDisponiveis(string permissao)
        {
            return contexto.SqlBd.Query<Evento>("select e.Id,e.Local,e.Tema,e.DiaInicial,e.DiaFinal,c.Nome NomeCoordenador from evento e,coordenador c where e.Status = @role and c.Id = e.CoordenadorId", new { role = permissao }).ToList();
        }
        public List<Evento> MeusEventos(string coordenadorId)
        {
            var listaDeEventosParaCoordeandor = contexto.SqlBd.Query<Evento>(
                 "select e.Id as Id,e.Local,e.Tema,e.DiaInicial,e.DiaFinal from Evento e where @CoordenadorId = e.CoordenadorId",
                 new
                 {
                     CoordenadorId = coordenadorId
                 }).ToList();
            return listaDeEventosParaCoordeandor;
        }
        public IEnumerable<PalestraSolicitadaViewModel> ListarPalestraDesseEvento(string TrilhaId, string CoordenadorId)
        {
            return contexto.SqlBd.Query<PalestraSolicitadaViewModel>("select p.Titulo,p.Id PalestraId,pp.NomePalestrante Nome,pp.Id PalestranteId,pp.EmailPalestrante Email,ps.Pendencia,ps.EventoId  EventoId " +
                                   "from Palestra p,Palestrante pp, PalestraSolicitada ps,trilha t,Coordenador c " +
                                   "where p.Id=ps.PalestraId and " +
                                   "pp.Id=ps.PalestranteId and " +
                                   "t.Id=ps.TrilhaId and " +
                                   "c.Id=ps.CoordenadorId and " +
                                   "ps.TrilhaId=@trilhaId and ps.CoordenadorId =@coordenadorId and ps.Pendencia != 'recusada' ", new { trilhaId = TrilhaId, coordenadorId = CoordenadorId }).ToList();
        }
        public IEnumerable<Evento> EventosSolicidados(string palestranteId)
        {
            return contexto.SqlBd.Query<Evento>(
                  "select Id,Tema,Local,DiaInicial,DiaFinal from evento e,palestrasolicitada ps where ps.EventoId=e.Id and ps.PalestranteId= @pId group by(Tema)",
                  new { pId = palestranteId }).ToList();
        }
        public void ResponderRequisicao(string palestraId, string eventoId, string resposta, string coordenadorId, string trilhaId, string palestranteId)
        {
            if (resposta == "pendente" || resposta == "recusada" || resposta == "aceita" || resposta == "confirmada")
            {
                contexto.SqlBd.Query(
                "update palestrasolicitada set Pendencia = @R where PalestraId=@pId and EventoId = @eId", new
                {
                    R = resposta,
                    pId = palestraId,
                    eId = eventoId
                });
            }


        }
        public IEnumerable<PalestraViewModel> PalestrasDisponiveisEvento(string eventoId, string usuarioId)
        {
            var todasPalestras = contexto.SqlBd.Query<PalestraViewModel>(
                 "select p.Id ,p.Titulo,pl.Id PalestranteId,pl.NomePalestrante,s.Id SalaId,pe.Vagas,s.NumeroSala,t.Id TrilhaId,t.NomeTrilha ,pe.HorarioInicial,pe.HorarioFinal,pe.EventoId   " +
                 "from evento e ,palestrante pl,palestra p,sala s,trilha t,palestraevento pe " +
                 "where e.Id=pe.EventoId and " +
                 "pl.Id=p.PalestranteId and " +
                 "p.Id = pe.PalestraId and " +
                 "t.Id= pe.TrilhaId and " +
                 "s.Id =pe.SalaId and " +
                 "pe.EventoId = @Id", new { Id = eventoId }).ToList();
            var minhasPalestras = PalestrasAdicionadas(eventoId, usuarioId);
            var palestraVM = new List<PalestraViewModel>();
            foreach (var palestras in todasPalestras)
            {
                var tempPalestra = minhasPalestras.FirstOrDefault(x => x.Id == palestras.Id);
                if (tempPalestra == null)
                {
                    palestraVM.Add(palestras);
                }
            }

            return palestraVM;
        }

        public IEnumerable<PalestraSolicitadaViewModel> PalestrasRegistradas(string eventoId, string coordenadorId)
        {
            return contexto.SqlBd.Query<PalestraSolicitadaViewModel>("select p.Titulo,p.Id palestraId,pe.EventoId EventoId,t.NomeTrilha,t.Id trilhaId,p.PalestranteId ,pp.NomePalestrante Nome,pp.EmailPalestrante Email,pe.HorarioInicial HoraInicial,pe.HorarioFinal HoraFinal from " +
                                                                     "Palestra p,Trilha t,PalestraEvento pe,Palestrante pp " +
                                                                     "where p.Id = pe.PalestraId " +
                                                                     "and t.Id = pe.TrilhaId " +
                                                                     "and p.PalestranteId = pp.Id " +
                                                                     "and pe.EventoId = @eId and pe.CoordenadorId = @cId",
                                                                     new { eId = eventoId, cId = coordenadorId }).ToList();
        }

        public PalestraSolicitadaViewModel DetalhePalestra(string palestraId, string coordenadorId, string eventoId)
        {
            var palestra = contexto.SqlBd.Query<PalestraSolicitadaViewModel>(
                 "select pe.Vagas,pe.Dia, pe.SalaId SalaId,pe.HorarioInicial HoraInicial,pe.HorarioFinal HoraFinal,p.Titulo,p.Id palestraId,pe.EventoId EventoId,t.NomeTrilha,t.Id trilhaId,p.PalestranteId ,pp.NomePalestrante Nome,pp.EmailPalestrante Email from " +
                 "Palestra p,Trilha t,PalestraEvento pe,Palestrante pp " +
                 "where p.Id = pe.PalestraId " +
                 "and t.Id = pe.TrilhaId " +
                 "and p.PalestranteId = pp.Id " +
                 "and pe.EventoId = @eId and pe.CoordenadorId = @cId and pe.PalestraId = @pId",
                 new { pId = palestraId, cId = coordenadorId, eId = eventoId }).FirstOrDefault();
            return palestra;
        }

        public void PreencherPalestraParaEvento(PalestraSolicitadaViewModel entidade)
        {
            contexto.SqlBd.Query(
                "update palestraevento set HorarioInicial = @hi , HorarioFinal = @hf , SalaId = @sId,Vagas = @vagas,Dia = @dia " +
                "where CoordenadorId = @cId and PalestraId = @pId and EventoId = @eId",
                new
                {
                    hi = entidade.HoraInicial,
                    hf = entidade.HoraFinal,
                    sId = entidade.SalaId,
                    cId = entidade.CoordenadorId,
                    pId = entidade.PalestraId,
                    eId = entidade.EventoId,
                    vagas = entidade.Vagas,
                    dia = entidade.Dia
                });
        }

        public bool ConfirmarParticipacao(string palestraId, string eventoId, string resposta, string coordenadorId, string trilhaId, string palestranteId)
        {
            bool validacao = false;
            if (resposta == "confirmada")
            {
                var _eventosParticipados = contexto.SqlBd.Query<Evento>("select e.Tema,e.Id,DiaInicial,DiaFinal from palestraevento ps ,evento e where e.Id=ps.EventoId and ps.PalestraId = @pId group by e.Tema ", new { pId = palestraId }).ToList();
                var _todosEventosDisponiveis = contexto.SqlBd.Query<Evento>("select Id,DiaInicial,DiaFinal,Tema from evento").ToList();
                foreach (var evento in _todosEventosDisponiveis)
                {
                    foreach (var meusEventos in _eventosParticipados)
                    {
                        if (meusEventos.ID != evento.ID)
                        {
                            foreach (var meuEvento in _eventosParticipados)
                            {
                                if (meuEvento.ID != evento.ID)
                                {
                                    validacao = ChecaAnoMesDia(meuEvento.DiaInicial, meuEvento.DiaFinal,
                                        evento.DiaInicial, evento.DiaFinal);
                                    if (validacao == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (validacao == false)
                {
                    contexto.SqlBd.Query(
                "update palestrasolicitada set Pendencia = @R where PalestraId=@pId and EventoId = @eId", new
                {
                    R = resposta,
                    pId = palestraId,
                    eId = eventoId
                });




                    contexto.SqlBd.Query(
                    "insert into palestraevento (EventoId,PalestraId,CoordenadorId,TrilhaId) values(@eId,@pId,@cId,@tId)",
                    new { eId = eventoId, pId = palestraId, cId = coordenadorId, tId = trilhaId });
                }

            }
            return validacao;
        }

        private bool ChecaAnoMesDia(DateTime dataInicial, DateTime dataFinal, DateTime dataInicial2, DateTime dataFinal2)
        {
            for (int ano = dataInicial.Year; ano <= dataFinal.Year; ano++)
            {
                for (int ano2 = dataInicial2.Year; ano2 <= dataFinal2.Year; ano2++)
                {
                    if (ano == ano2)
                    {
                        for (int meses1 = dataInicial.Month; meses1 <= dataFinal.Month; meses1++)
                        {
                            for (int meses2 = dataInicial2.Month; meses2 <= dataFinal2.Month; meses2++)
                            {
                                if (meses1 == meses2)
                                {

                                    for (int dias1 = dataInicial.Day; dias1 <= dataFinal.Day; dias1++)
                                    {
                                        for (int dias2 = dataInicial2.Day; dias2 <= dataFinal2.Day; dias2++)
                                        {
                                            if (dias1 == dias2)
                                            {
                                                return true;
                                            }
                                        }

                                    }
                                }
                            }

                        }

                    }
                }
            }
            return false;
        }

        public void InscreverPalestraEvento(string palestraId, string eventoId, string usuarioId, bool status, string trilhaId, string salaId)
        {
            contexto.SqlBd.Query("insert into PalestraUsuario (PalestraId,EventoId,UsuarioId,Status,TrilhaId,SalaId) values (@pid,@eid,@uid,@st,@tid,@sid)",
                new
                {
                    pid = palestraId,
                    eid = eventoId,
                    uid = usuarioId,
                    st = status,
                    tid = trilhaId,
                    sid = salaId
                });
        }

        public List<PalestraViewModel> PalestrasAdicionadas(string id, string usuarioId)
        {
            return contexto.SqlBd.Query<PalestraViewModel>(
                  "select p.Id ,p.Titulo,s.Id SalaId,s.NumeroSala,t.Id TrilhaId,t.NomeTrilha ,pe.EventoId,ep.HorarioInicial,ep.HorarioFinal,ep.Dia,pe.Status   " +
                  "from evento e ,palestra p,sala s,trilha t,palestrausuario pe ,palestraevento ep " +
                  "where e.Id=pe.EventoId and " +
                  "p.Id = pe.PalestraId and " +
                  "t.Id= pe.TrilhaId and " +
                  "s.Id =pe.SalaId and " +
                  "ep.EventoId = e.Id and ep.SalaId = s.Id and ep.PalestraId = p.Id and ep.TrilhaId = t.Id and " +
                  "pe.EventoId = @Id and pe.UsuarioId= @uid", new { Id = id, uid = usuarioId }).ToList();

        }
        public IEnumerable<Evento> EventosUsuario(string usuarioID)
        {
            return contexto.SqlBd.Query<Evento>(
                  "select e.Id as Id,e.Local,e.Tema,e.DiaInicial,e.DiaFinal from Evento e ,palestraUsuario pu where e.Id=pu.EventoId and pu.UsuarioId = @uid group by e.Tema",
                  new
                  {
                      uid = usuarioID
                  }).ToList();

        }

        public bool ConfirmarParticipacaoUsuario(string eventoId, string palestraId, string usuarioId)
        {
            #region MinhasPalestras


            var minhasPalestras = contexto.SqlBd.Query<PalestraViewModel>(
                 "select p.Id ,p.Titulo,s.Id SalaId,s.NumeroSala,t.Id TrilhaId,t.NomeTrilha ,pe.EventoId,ep.HorarioInicial,ep.HorarioFinal,ep.Dia   " +
                 "from evento e ,palestra p,sala s,trilha t,palestrausuario pe ,palestraevento ep " +
                 "where e.Id=pe.EventoId and " +
                 "p.Id = pe.PalestraId and " +
                 "t.Id= pe.TrilhaId and " +
                 "s.Id =pe.SalaId and " +
                 "ep.EventoId = e.Id and ep.SalaId = s.Id and ep.PalestraId = p.Id and ep.TrilhaId = t.Id and " +
                 "pe.UsuarioId= @uid", new { uid = usuarioId }).ToList();
            #endregion

            var validacao = false;
            foreach (var palestras in minhasPalestras)
            {
                var palestra = minhasPalestras.FirstOrDefault(x => x.Id == palestras.Id && x.EventoId == eventoId);
                foreach (var palestras2 in minhasPalestras)
                {
                    if (palestra.Id != palestras2.Id)
                    {
                        if (palestra.Dia.Day == palestras2.Dia.Day)
                        {
                            if (palestra.Id != palestras2.Id)
                            {
                                for (int i = palestra.HorarioInicial.Hour; i <= palestra.HorarioFinal.Hour; i++)
                                {
                                    for (int j = palestras2.HorarioInicial.Hour; j <= palestras2.HorarioFinal.Hour; j++)
                                    {
                                        if (i == j)
                                        {
                                            validacao = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }


            }
            if (validacao == false)
            {
                contexto.SqlBd.Query("insert into usuariosConfirmados (EventoId,PalestraId,UsuarioId) values(@eid,@pid,@uid)", new
                                                         {
                                                             eid = eventoId,
                                                             pid = palestraId,
                                                             uid = usuarioId
                                                         });
                int vagas = contexto.SqlBd.Query<int>("select p.Vagas from palestraevento p where EventoId= @eid and PalestraId= @pid",new{eid=eventoId,pid=palestraId}).FirstOrDefault();
                if (vagas!=null || vagas>=1)
                {
                    contexto.SqlBd.Query("update palestraevento set Vagas = @vaga where Eventoid= @eid and PalestraId = @pid" , new {vaga = (vagas-1),eid=eventoId,pid=palestraId});
                    contexto.SqlBd.Query("update palestrausuario set Status = '0' where EventoId= @eid and PalestraId = @pid and UsuarioId= @uid",new{eid=eventoId,pid=palestraId,uid=usuarioId});
                }
                return true;
            }

            return false;
        }

        public IEnumerable<UsuariosConfirmadosVM> RelatorioPalestras(string e,string u)
        {
            return contexto.SqlBd.Query<UsuariosConfirmadosVM>("select e.Tema,p.Titulo,pe.HorarioInicial,pe.HorarioFinal,s.NumeroSala Sala,S.Descricao from evento e ,palestra p,palestraevento pe,usuariosconfirmados uc,sala s where uc.EventoId=e.Id and uc.PalestraId=p.Id and pe.salaId=s.Id and pe.eventoId=e.Id and pe.PalestraId = p.Id and uc.usuarioId = @uid and uc.EventoId = @eid", new {uid = u ,eid=e}).ToList();

        }

        public IEnumerable<UsuariosConfirmadosVM>UsuarioConfirmados(string eventoId)
        {
            return contexto.SqlBd.Query<UsuariosConfirmadosVM>("SELECT u.Foto,u.NomeUsuario,u.EmailUsuario,e.Tema,p.Titulo,pe.HorarioInicial,pe.HorarioFinal from evento e,usuario u,palestra p,usuariosconfirmados uc,palestraevento pe where uc.eventoId=e.Id and uc.usuarioid=u.id and uc.palestraid = p.id and uc.EventoId = @eid group by u.NomeUsuario order by p.Titulo",new{eid=eventoId}).ToList();
        }

        public IEnumerable<PalestranteVM> PalestrantesEvento(string _eventoId)
        {
            return contexto.SqlBd.Query<PalestranteVM>(
                "select p.Id,p.NomePalestrante Nome,p.EmailPalestrante Email,p.Foto PhotoPath from palestrante p,palestraevento pe,palestra pl where pe.palestraId= pl.Id and pl.PalestranteId = p.Id and pe.EventoId = @eid group by(p.Id)",
                new {eid = _eventoId}).ToList();
        }
    }
}