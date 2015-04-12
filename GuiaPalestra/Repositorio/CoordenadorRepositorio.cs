using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Aplicacao;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Repositorio
{
    public class CoordenadorRepositorio:ICrud<Coordenador>
    {
        private readonly Contexto contexto;
        public CoordenadorRepositorio()
        {
            contexto=new Contexto();
        }
        public void Save(Coordenador entidade)
        {
            contexto.SqlBd.Query(
                "insert into coordenador (Id,Nome,Email,Senha,Permissao) values(@id,@name,@email,@password,'coordenador')",
                new {id = entidade.ID, name = entidade.Nome, email = entidade.Email, password = entidade.Senha});
        }

        public void Update(Coordenador entidade)
        {
            contexto.SqlBd.Query("update coordenador set Nome = @name , Email = @email where Id = @id ",
                new {name = entidade.Nome, email = entidade.Email, id = entidade.ID});
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Coordenador GetByID(string Id)
        {
            return contexto.SqlBd.Query<Coordenador>("select Id,Nome,Email from coordenador where Id = @id",new{id=Id}).FirstOrDefault();
        }

        public IEnumerable<Coordenador> GetAll()
        {
            return contexto.SqlBd.Query<Coordenador>("select Id,Nome,Email from coordenador ").ToList();
            
        }

    }
}