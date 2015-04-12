using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using GuiaPalestrasOnline.Helpers;
using GuiaPalestrasOnline.Models;
using GuiaPalestrasOnline.Repositorio;

namespace GuiaPalestrasOnline.Aplicacao
{
    public class PalestranteApp 
    {
        private readonly ICrud<Palestrante> repositorio;
        private readonly Contexto contexto;
        public PalestranteApp(ICrud<Palestrante> repo)
        {
            contexto=new Contexto();
            repositorio=repo;
        }
        public IEnumerable<Palestrante> GetAll()
        {
            return repositorio.GetAll();
        }
        public void Save(Palestrante palestrante)
        {
            repositorio.Save(palestrante);
        }
        public Palestrante Details(string id)
        {
           return repositorio.GetByID(id);
        }
        public void Edit(Palestrante palestrant)
        {
            repositorio.Update(palestrant);
        }
        public void Remove(string Id)
        {
            repositorio.Delete(Id);
        }

        public Usuario LogarPalestrante(string email, string senha)
        {
            var tempPalestrante=contexto.SqlBd.Query<dynamic>(
                "select nomepalestrante as Nome,Id,emailPalestrante as Email,Permissao from palestrante where emailPalestrante = @Email and Senha = @Senha",
                new {Email = email, Senha = senha}).FirstOrDefault();
            if (tempPalestrante == null)
            return new Usuario();
            string permissoes = tempPalestrante.Permissao;
                var tempUsuario = new Usuario
                {
                    ID = tempPalestrante.Id,
                    Nome = tempPalestrante.Nome,
                    Email = tempPalestrante.Email,
                    Permissao = permissoes.Split(',').ToList()
                };
                return tempUsuario;
        }
    }
}