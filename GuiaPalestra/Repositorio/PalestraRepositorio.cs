using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestra.Repositorio
{
    public class PalestraRepositorio : ICrud<Palestra>
    {
        private readonly Contexto contexto;
        #region QueryParaListarTodasPalestras

        private readonly string Query =
            "select p.titulo,p.duracao,p.Id palestraId,pp.nomepalestrante,pp.Id palestranteId from palestra p ,palestrante pp where p.palestranteId=pp.Id and P.status= 1 ";

        #endregion
        public PalestraRepositorio()
        {
            contexto = new Contexto();
        }

        public void Save(Palestra entidade)
        {
            contexto.SqlBd.Query(
                "insert into palestra (Id,Titulo,Duracao,PalestranteId,TrilhaId,SalaId,Status) values(@id,@titulo,@duracao,@palestranteid,@trilhaid,@salaid,'0')",
                new
                {
                    id = entidade.ID,
                    titulo = entidade.Titulo,
                    duracao = entidade.Duracao,
                    palestranteid = entidade.PalestranteId,
                    trilhaid = entidade.TrilhaId,
                    salaid = entidade.SalaId
                });
        }

        public void Update(Palestra entidade)
        {
            contexto.SqlBd.Query(
                "update palestra set duracao = @duracao , trilhaid = @trilhaid, salaid = @salaid where id = @id",
                new
                {
                    duracao = entidade.Duracao,
                    salaid = entidade.SalaId,
                    trilhaid = entidade.TrilhaId,
                    id = entidade.ID
                });
        }
        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }
        public Palestra GetByID(string Id)
        {
            var palestraGetById =
                contexto.SqlBd.Query<dynamic>(
                    Query + " and p.Id = @id", new { id = Id })
                    .FirstOrDefault();
            var palestra = new Palestra
            {
                ID = palestraGetById.palestraId,
                Titulo = palestraGetById.titulo,
                Palestrante = new Palestrante { ID = palestraGetById.palestranteId, Nome = palestraGetById.nomepalestrante },
                Trilha = new Trilha { ID = palestraGetById.trilhaId, NomeTrilha = palestraGetById.nometrilha },
                Sala = new Sala { ID = palestraGetById.salaId, NumeroSala = palestraGetById.numeroSala }
            };

            return palestra;
        }
        public IEnumerable<Palestra> GetAll()
        {
            var palestras =
                contexto.SqlBd.Query<dynamic>(Query)
                    .ToList();

            return palestras.Select(item => new Palestra()
            {
                ID = item.palestraId, Titulo = item.titulo, Duracao = item.duracao, Palestrante = new Palestrante {ID = item.palestranteId, Nome = item.nomepalestrante}, Trilha = new Trilha {ID = item.trilhaId, NomeTrilha = item.nometrilha}, Sala = new Sala {ID = item.salaId, NumeroSala = item.numeroSala}
            }).ToList();
        }
        public static IEnumerable<Palestra> GetAll(string id)
        {
            var palestras = new Contexto().SqlBd.Query(
                   "select p.titulo,p.duracao,p.Id palestraId,pp.nomepalestrante,pp.Id palestranteId,t.nometrilha,t.Id trilhaId,s.numeroSala,s.Id salaId from palestra p ,palestrante pp, trilha t ,sala s, palestrasusuario pu where p.palestranteId=pp.Id and p.salaId= s.Id and p.trilhaId = t.Id and pu.usuarioId = @idUsuario and pu.palestraId=p.Id and p.Status = 1", new { @idUsuario = id }).ToList();
            var listasPalestras = new List<Palestra>();
            foreach (var item in palestras)
            {
                var palestra = new Palestra()
                {
                    ID = item.palestraId,
                    Titulo = item.titulo,
                    Palestrante = new Palestrante { ID = item.palestranteId, Nome = item.nomepalestrante },
                    Trilha = new Trilha { ID = item.trilhaId, NomeTrilha = item.nometrilha },
                    Sala = new Sala { ID = item.salaId, NumeroSala = item.numeroSala }
                };
                listasPalestras.Add(palestra);
            }

            return listasPalestras;
        }
    }
}