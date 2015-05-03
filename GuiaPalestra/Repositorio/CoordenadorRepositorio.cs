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
                "insert into coordenador (Id,Nome,Email,Senha,Permissao,Foto) values(@id,@name,@email,@password,'coordenador','GenericPhoto.png')",
                new {id = entidade.ID, name = entidade.Nome.ToLower(), email = entidade.Email.ToLower(), password = entidade.Senha.ToLower()});
        }

        public void Update(Coordenador entidade)
        {
            contexto.SqlBd.Query("update coordenador set Nome = @name , Email = @email, Foto = @photo where Id = @id ",
                new {name = entidade.Nome.ToLower(), email = entidade.Email.ToLower(), id = entidade.ID,photo=entidade.Foto});
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Coordenador GetByID(string Id)
        {
            return contexto.SqlBd.Query<Coordenador>("select Id,Nome,Email,Foto from coordenador where Id = @id",new{id=Id}).FirstOrDefault();
        }

        public IEnumerable<Coordenador> GetAll()
        {
            return contexto.SqlBd.Query<Coordenador>("select Id,Nome,Email from coordenador ").ToList();
            
        }

    }
}