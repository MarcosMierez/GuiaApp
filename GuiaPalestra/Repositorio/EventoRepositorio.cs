using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Repositorio
{
    public class EventoRepositorio:ICrud<Evento>
    {
        private readonly Contexto contexto;

        public EventoRepositorio()
        {
           contexto=new Contexto();
        }
        public void Save(Evento entidade)
        {
            contexto.SqlBd.Query("insert into Evento (Id,DiaInicial,DiaFinal,Local,CoordenadorId,Tema) values(@Id,@DI,@DF,@local,@cId,@tema)",
                new
                {
                    Id = entidade.ID,
                    Day = entidade.DiaInicial,
                    DI=entidade.DiaInicial,
                    Df=entidade.DiaFinal,
                    local=entidade.Local,
                    cId=entidade.CoordenadorId,
                    tema=entidade.Tema
                });
        }

        public void Update(Evento entidade)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Evento GetByID(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> GetAll()
        {
            return contexto.SqlBd.Query<Evento>("select Id,Local,Tema,DiaInicial,DiaFinal,CoordenadorId from Evento").ToList();
        }

    }
}